using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
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
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    return cookie.Value.ToEncryptedString().AsInt();
                }
                return -1;
            }

            set
            {
                HttpCookie cookie = new HttpCookie(UseridCookieKey, value.ToString().ToEncryptedString());
                cookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public List<Model.Permission> CurrentUserPermissions
        {
            get { return HttpContext.Current.Session[UserPerSessionKey] as List<Model.Permission>; }
            set { HttpContext.Current.Session[UserPerSessionKey] = value; }
        }

        public bool HasPermission(string AreaName, string ControllerName, string ActionName, string FormMethod)
        {
            var hasPermission = GetUserPermission(AreaName, ControllerName, ActionName, FormMethod) != null;
            return hasPermission;
        }

        public Model.Permission GetUserPermission(string areaName, string controllerName, string actionName, string formMethod)
        {
            int intFormMethod = formMethod.ToLower() == "get" ? 1 : 2;
            var currentUserPermission = CurrentUserPermissions.SingleOrDefault(
                (p => p.permissionAreaName.ToLower().Trim() == areaName.ToLower().Trim()
                    && p.permissionControllerName.ToLower().Trim() == controllerName.ToLower().Trim()
                    && p.permissionActionName.ToLower().Trim() == actionName.ToLower().Trim()
                    && (p.permissionFormMethod == 3 || (p.permissionFormMethod == intFormMethod))));

            return currentUserPermission;

        }


        public JsonResult SendAjaxMessage(AjaxMessageStatus status, string message, string backUrl, object data)
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
                Data = ajaxMessage,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ContentResult SendJsMessage(string message, string backUrl)
        {
            StringBuilder stringBuilder = new StringBuilder("<script> alert(\"").Append(message).Append("\");");
            if (!string.IsNullOrEmpty(backUrl))
            {
                stringBuilder.Append("if(window.top!=window)window.top.location=\"").Append(backUrl).Append("\";");
                stringBuilder.Append("else window.location=\"").Append(backUrl).Append("\";");
            }
            stringBuilder.Append("</script>");
            return new ContentResult()
            {
                Content = stringBuilder.ToString()
            };
        }

    }
}