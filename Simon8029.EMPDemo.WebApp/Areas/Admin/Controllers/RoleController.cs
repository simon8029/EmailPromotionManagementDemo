using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Newtonsoft.Json;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();
            var pageData = OperationContext.ServiceSession.RoleService.GetWithPagination(pageIndex, pageSize, o => o.roleIsDeleted == false, o => o.roleID, true);
            pageData.rows = pageData.rows.Select(o => o.ToPOCO()).ToList();
            var jsSerializer = new JavaScriptSerializer();

            var strJson = jsSerializer.Serialize(pageData);
            return Content(strJson);
        }

        [HttpGet]

        public ActionResult SetRolePermission(int id)
        {
            SetRolePermissionViewModel viewModel = new SetRolePermissionViewModel();
            viewModel.RoleID = id;
            viewModel.AllPermissions = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionIsDeleted == false).ToList();
 
            viewModel.RolePermissions = OperationContext.ServiceSession.RolePermissionRelationshipService.Get(o => o.roleID == id).Select(o => o.Permission).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SetRolePermission()
        {
            int roleId = Request.Form["rid"].AsInt();
            List<int> newPermissionIdList = Request.Form["newPermissionIds"].Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.AsInt()).ToList();
            var oldPerList = OperationContext.ServiceSession.RolePermissionRelationshipService.Get(o => o.roleID == roleId).ToList();
     
            for (int i = oldPerList.Count - 1; i >= 0; i--)
            {
                var oldPer = oldPerList[i];
                if (newPermissionIdList.Contains(oldPer.permissionID))
                {
                    newPermissionIdList.Remove(oldPer.permissionID);
                    oldPerList.Remove(oldPer);
                }
            }
            newPermissionIdList.ForEach(newPerId =>
            {
                OperationContext.ServiceSession.RolePermissionRelationshipService.Add(new Model.RolePermissionRelationship() { roleID = roleId, permissionID = newPerId });
            });
            oldPerList.ForEach(oldPer =>
            {
                OperationContext.ServiceSession.RolePermissionRelationshipService.Delete(oldPer);
            });
            OperationContext.ServiceSession.SaveChange();
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
        }


        [HttpGet]
        public ActionResult Add()
        {
            var departmentId = OperationContext.ServiceSession.DepartmentService.Get(d => d.departmentIsDeleted==false , d => d.departmentID).ToList().Select(d => new SelectListItem()
            {
                Text = d.departmentName,
                Value = d.departmentID.ToString()
            });
            ViewBag.DepartmentId = departmentId;
            return View();
        }

  
        [HttpPost]
        public ActionResult Add(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.RoleService.Add(viewModel.ToPOCO());
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            else
            {
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in browser.", "", null);
            }
        }



        [HttpGet]
        public ActionResult Modify(int id)
        {
            var modifyData = OperationContext.ServiceSession.RoleService.Get(o => o.roleID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the role."); }

            var departmentId = OperationContext.ServiceSession.DepartmentService.Get(o => o.departmentIsDeleted == false , o => o.departmentID).ToList().Select(o => new SelectListItem()
            {
                Text = o.departmentName,
                Value = o.departmentID.ToString()
            });
            ViewBag.DepartmentId = departmentId;

           // return View(modifyData.ToViewModel());
            return View(modifyData.ToViewModel());
        }

        [HttpPost]
  
        public ActionResult Modify(int id, RoleViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.roleID =id ;
                OperationContext.ServiceSession.RoleService.Update(viewmodel.ToPOCO(), "roleName",  "roleIsDeleted");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }
    }
}
