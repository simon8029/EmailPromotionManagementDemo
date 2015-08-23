using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.Utilities;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models.ViewModels;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.Utilities.Attributes;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet,SkipLoginCheck]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost,SkipLoginCheck]
        public ActionResult Login(LoginWindowViewModel loginUserInfo)
        {

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
                        return OperationContext.SendAjaxMessage(AjaxMessageStatus.LoginFailed, "User name does not exist.", "Admin/User/Login", null);
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

                        return OperationContext.SendAjaxMessage(AjaxMessageStatus.LoginSuccess, "Login success.",
                            "/Admin/Manage/Index", null);
                    }

                    return OperationContext.SendAjaxMessage(AjaxMessageStatus.LoginFailed, "Password is not correct.",
                        "Admin/User/Login",
                        null);
                }
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.LoginFailed, "Validate code is not correct.", "Admin/User/Login", null);
            }

            return OperationContext.SendAjaxMessage(AjaxMessageStatus.LoginFailed,
                        "Please enable javascript in browser.", "", null);
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Index(FormCollection form)
        //{
        //    return View();
        //}
    }
}