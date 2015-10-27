using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class PermissionController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ChildIndex(int id)
        {
            ViewBag.pid = id;
            return View();
        }


        [HttpPost]
        public ActionResult Index(int id)
        {
            //MODEL.FormatMODEL.PageData<MODEL.Permission> page
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();

            EasyUIModel_PageData<Permission> permissions = null;
            if (id==1)//display all permissions
            {
                 permissions = OperationContext.ServiceSession.PermissionService.GetWithPagination(pageIndex, pageSize, p => p.permissionIsDeleted == false, p => p.permissionOrder); //.Where(o => o.perIsDel == false).OrderBy(o => o.perOrder).ToList().Select(o=>o.ToPOCO());
            }
            else
            {
                permissions = OperationContext.ServiceSession.PermissionService.GetWithPagination(pageIndex, pageSize, p => p.permissionIsDeleted == false && p.permissionParentID == id, p => p.permissionOrder); //.Where(o => o.perIsDel == false).OrderBy(o => o.perOrder).ToList().Select(o=>o.ToPOCO()); 
            }
            
            permissions.rows = permissions.rows.Select(o => o.ToPOCO()).ToList();

            var jsSerializer = new JavaScriptSerializer();
            var strJson = jsSerializer.Serialize(permissions);
            return Content(strJson);
        }

   
        [HttpGet]
        public ActionResult Add()
        {
           
            var parPers = OperationContext.ServiceSession.PermissionService.Get(p => p.permissionIsDeleted == false && p.permissionIsShow == true  , p => p.permissionOrder).ToList().Select(p => new SelectListItem()
            {
                Text = p.permissionParentID == 0 ? p.permissionName : "--" + p.permissionName,
                Value = p.permissionID.ToString()
            });
            ViewBag.parPers = parPers;
            return View();
        }

 
        [HttpPost]
        public ActionResult Add(PermissionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.PermissionService.Add(viewModel.ToPOCO());
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            else
            {
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in browser.", "", null);
            }
        }


        [HttpPost]
  
        public string LoadOrderNumber()
        {
            int newOrderNo = -1;
            //1.get parent permission id
            int permissionId = Request.Form["pId"].AsInt();
            //1.1get parent permission 
            var parentPermission = OperationContext.ServiceSession.PermissionService.Get(p => p.permissionID == permissionId).FirstOrDefault();
            //2.get max order child permission
            var maxOrderChildPermission = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionParentID == permissionId).OrderByDescending(o => o.permissionOrder).FirstOrDefault();

            
            int seed = 1;
            if (parentPermission.permissionParentID == 0)
            {
                seed = 100000;
            }
  
            if (maxOrderChildPermission == null)
            {
                newOrderNo = parentPermission.permissionOrder + seed;
            }
            else
            {
                newOrderNo = maxOrderChildPermission.permissionOrder + seed;
            }

            return newOrderNo.ToString();
        }

        [HttpGet]
        public ActionResult Modify(int id)
        {
            var modifyData = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the permission."); }

            var parPers = OperationContext.ServiceSession.PermissionService.Get(p=> p.permissionIsDeleted == false && p.permissionIsShow == true, o => o.permissionOrder).ToList().Select(p => new SelectListItem()
            {
                Text = p.permissionParentID == 0 ? p.permissionName : "--" + p.permissionName,
                Value = p.permissionID.ToString()
            });
            ViewBag.parPers = parPers;

            return View(modifyData.ToViewModel());
        }

        [HttpPost]

        public ActionResult Modify(int id, PermissionViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.PermissionId = id;
                OperationContext.ServiceSession.PermissionService.Update(viewmodel.ToPOCO(), "permissionParentID", "permissionName", "permissionRemark", "permissionAreaName", "permissionControllerName", "permissionActionName", "permissionFormMethod", "permissionOperationType", "permissionJSMethodName", "permissionIcon", "permissionIsLink", "permissionOrder", "permissionIsShow");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }
    }

}