using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp;
using Simon8029.EMPDemo.Utilities.Attributes;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Filters
{
   
    public class CheckPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private OperationContext operationContext = new OperationContext();

       
        List<string> blackAreaNames = new List<string>() { "Admin","EmailMarketing" };

        
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //detect if current url has area name
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                //0.2 get current area name
                string strCurAreaName = filterContext.RouteData.DataTokens["area"].ToString();
                // if current area is in the blacklist, then detect permission
                if (blackAreaNames.Contains(strCurAreaName))
                {
                    // if  [skiplogin] then skip login check
                    if (!IsDefind<SkipLoginCheckAttribute>(filterContext))
                    {
                        
                        if (IsLogin())
                        {
                            filterContext.Controller.ViewBag.CurrentUserName =
                                operationContext.CurrentUser.employeeLoginName;
                            LoadMenuButtons(filterContext);
                            // if [skipPermission] then skip permission check
                            if (!IsDefind<SkipPermissionCheckAttribute>(filterContext))
                            {
                                
                                //2. check if user has permisison to access current url
                                if (!operationContext.HasPermission(strCurAreaName,
                                     filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                     filterContext.ActionDescriptor.ActionName,
                                     HttpContext.Current.Request.HttpMethod))
                                {
                                    filterContext.Result = SendMessage("sorry, you don't have the permission. please login with other account.", "/Admin/User/Login");
                                }
                                else
                                {
                                    //LoadMenuBtns(filterContext);
                                }
                            }
                            else
                            {
                                //LoadMenuBtns(filterContext);
                            }
                        }
                        else
                        {
                            filterContext.Result = SendMessage("Please login first.", "/Admin/User/Login");
                        }
                    }
                }
            }

        }

        #region 1.0 check if current user is login 
        private bool IsLogin()
        {
            //1. check if has session
            if (operationContext.CurrentUser == null)
            {
                //1.1 if no session, check cookie
                //1.1.1 if no cookie, means user is not login, return false. 
                if (operationContext.CurrentUserIdInCookie <= -1)
                {
                    return false;
                }
                //1.1.2 if has cookie, login user by id in cookie, then save to session
                else
                {
                    var usrId = operationContext.CurrentUserIdInCookie;
                    operationContext.CurrentUser = operationContext.ServiceSession.EmployeeService.Get(o => o.employeeID == usrId).SingleOrDefault().ToPOCO();
                    operationContext.CurrentUserPermissions = operationContext.ServiceSession.EmployeeService.GetUserPermissions(usrId);
                }
            }
            return true;
        }
        #endregion

        #region 2.0 检查 过滤器上下文 中的当前被请求的方法 和 控制器 是否有贴标签 -bool IsDefind<AttrType>(System.Web.Mvc.AuthorizationContext filterContext)
        /// <summary>
        /// 检查 过滤器上下文 中的当前被请求的方法 和 控制器 是否有贴标签
        /// </summary>
        /// <typeparam name="TAttributeType">要检查的标签类型</typeparam>
        /// <param name="filterContext">过滤器上下文</param>
        /// <returns></returns>
        bool IsDefind<TAttributeType>(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //获取要检查的标签 的 类型对象
            Type attrTypeObj = typeof(TAttributeType);
            //分别检查 被请求的方法 和 控制器上 是否有贴 指定的标签，如果任意贴了，则返回true
            return filterContext.ActionDescriptor.IsDefined(attrTypeObj, false)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attrTypeObj, false);
        }
        #endregion

        #region 3.0 根据是否为异步请求 返回不同的消息 +ActionResult SendMsg(string strMsg, string strBackUrl)
        /// <summary>
        /// 3.0 根据是否为异步请求 返回不同的消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="backUrl"></param>
        /// <returns></returns>
        System.Web.Mvc.ActionResult SendMessage(string message, string backUrl)
        {
            //1.判断请求报文中是否包含 X-Requested-With: XMLHttpRequest
            //1.1如果包含，则代表是 浏览器端通过 jquery异步方法 创建的 异步对象请求的
            if (HttpContext.Current.Request.Headers.AllKeys.Contains("X-Requested-With"))
            {
                return operationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, message, backUrl,null);
            }
            //1.2如果不包含，则代表是浏览器直接请求的
            else
            {
                return operationContext.SendJsMessage(message, backUrl);
            }

            
        }
        #endregion

        #region 4.0 根据当前访问的页面 查找 登录用户的 子按钮权限 +void LoadMenuBtns(System.Web.Mvc.AuthorizationContext filterContext)
        /// <summary>
        /// 根据当前访问的页面 查找 登录用户的 子按钮权限
        /// </summary>
        void LoadMenuButtons(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //1.获取当前请求url数据
            string strCurAreaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            string strControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string strActionName = filterContext.ActionDescriptor.ActionName;
            //1.1根据当前访问url找到 登录用户的 菜单权限（到登录用户的Session中存放的权限集合中）
            Permission menuPermission = operationContext.GetUserPermission(strCurAreaName, strControllerName, strActionName, HttpContext.Current.Request.HttpMethod);
            //1.2如果存在此权限在，则加载用户 在此页面的 按钮集合
            if (menuPermission != null)
            {
                //2.再根据菜单权限 去 当前登录用户Session的 权限集合中 查找 子按钮权限集合
                var buttons = operationContext.CurrentUserPermissions.Where(o => o.permissionParentID == menuPermission.permissionID && o.permissionOperationType == EnumHelper.OperationType.BUTTON && o.permissionIsDeleted == false).OrderBy(o => o.permissionOrder).ToList();
                //4.如果 登录用户 没有任何 该页面的  子按钮权限，就设置一个空的权限集合
                if (buttons == null)
                    filterContext.Controller.ViewBag.toolbarButtons = emptyButtons;
                else
                    //5.存入 ViewBag
                    filterContext.Controller.ViewBag.toolbarButtons = buttons;
            }
            else
            {
                filterContext.Controller.ViewBag.toolbarButtons = emptyButtons;
            }
        }
        #endregion

        static List<Permission> emptyButtons = new List<Permission>();
    }
}