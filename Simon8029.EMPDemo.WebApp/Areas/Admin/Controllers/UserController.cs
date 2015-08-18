using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models.ViewModels;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginWindowViewModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}