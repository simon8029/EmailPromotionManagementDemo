using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.Utilities;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models.ViewModels;
using Simon8029.EMPDemo.WebApp.Controllers;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginWindowViewModel loginUserInfo)
        {
            var str = "123123".ToMD5();
            if (ModelState.IsValid)
            {
                if (Session[VCode.vCodeName] != null &&
                    loginUserInfo.LoginValidateCode.IsSame(Session[VCode.vCodeName].ToString()))
                {
                    var userInfoInDatabase =
                        OperationContext.ServiceSession.EmployeeService.Get(
                            e => e.employeeLoginName == loginUserInfo.LoginName).FirstOrDefault();

                    if (userInfoInDatabase == null)
                    {
                        return OperationContext.AjaxMessage(AjaxMessageStatus.LoginFailed, "User name does not exist.",
                            "", null);
                    }

                    if (loginUserInfo.LoginPassword.IsSame(userInfoInDatabase.employeeLoginPassword))
                    {
                        OperationContext.CurrentUser = userInfoInDatabase.ToPOCO();

                        if (loginUserInfo.RememberMe)
                        {
                            OperationContext.CurrentUserIdInCookie = userInfoInDatabase.employeeID;
                        }

                        OperationContext.CurrentUserPermissions =
                            OperationContext.ServiceSession.EmployeeService.GetUserPermissions(
                                userInfoInDatabase.employeeID);

                        return OperationContext.AjaxMessage(AjaxMessageStatus.LoginSuccess, "Login success.",
                            "/admin/manage/index", null);
                    }

                    return OperationContext.AjaxMessage(AjaxMessageStatus.LoginFailed, "Password is not correct.",
                        "",
                        null);
                }
                return OperationContext.AjaxMessage(AjaxMessageStatus.LoginFailed, "Validate code is not correct.", "", null);
            }

            return OperationContext.AjaxMessage(AjaxMessageStatus.LoginFailed,
                        "Please enable javascript in browser.", "", null);
        }
    }
}