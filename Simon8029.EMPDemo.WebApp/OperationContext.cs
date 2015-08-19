using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Simon8029.EMPDemo.IService;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.Utilities;

namespace Simon8029.EMPDemo.WebApp
{
    public class OperationContext
    {
        public const string UserSessionKey = "USER_SESSION_KEY";
        public const string UserPerSessionKey = "USER_PER_SESSION_KEY";
        public const string UseridCookieKey = "USERID_COOKIE_KEY";

       IService.IServiceSession _serviceSession;
        public IService.IServiceSession ServiceSession
        {
            get
            {
                //1.如果为空
                if (_serviceSession == null)
                {
                    //2.实例化 业务仓储 对象
                    _serviceSession = Utilities.DI.GetObject<IServiceSession>("ServiceSession");
                }
                //3.返回业务仓储对象
                return _serviceSession;
            }
        }

        public Model.Employee CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[UserSessionKey] as Model.Employee;
            }
            set
            {
                HttpContext.Current.Session[UserSessionKey] = value;
            }
        }

        public int CurrentUserIdInCookie
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[UseridCookieKey];
                if (cookie!=null &&!string.IsNullOrEmpty(cookie.Value))
                {
                    return cookie.Value.ToEncryptedString().AsInt();
                }
                return -1;
            }

            set
            {
                HttpCookie cookie = new HttpCookie(UseridCookieKey,value.ToString().ToEncryptedString());
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public List<Model.Permission> CurrentUserPermissions
        {
            get { return HttpContext.Current.Session[UserPerSessionKey] as List<Model.Permission>; }
            set { HttpContext.Current.Session[UserPerSessionKey] = value; }
        }

        public bool HasPermission(string AreaName, string ControllerName, string ActionName, int FormMethod)
        {
            return GetUserPermission(AreaName, ControllerName, ActionName, FormMethod) != null;
        }

        private Model.Permission GetUserPermission(string areaName, string controllerName, string actionName, int formMethod)
        {
            return CurrentUserPermissions.SingleOrDefault
                (p => p.permissionAreaName == areaName 
                    && p.permissionControllerName == controllerName 
                    && p.permissionActionName == actionName 
                    && (p.permissionFormMethod == 3 || (p.permissionFormMethod == formMethod)));
        }

       
        public JsonResult AjaxMessage(AjaxMessageStatus status, string message, string backUrl, object data)
        {
            EasyUIModel_AjaxMessage ajaxMessage = new EasyUIModel_AjaxMessage()
            {
                Status = status,
                Message = message,
                BackUrl = backUrl,
                Data = data
            };
            return new JsonResult()
            {
                Data = ajaxMessage, JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}