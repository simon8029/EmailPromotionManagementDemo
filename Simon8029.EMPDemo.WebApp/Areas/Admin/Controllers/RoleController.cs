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
        #region 1.0 加载 列表 视图 +Index()
        /// <summary>
        /// 1.0 加载 列表 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 1.2 加载 列表 数据 +Index() +HttpPost
        /// <summary>
        /// 1.2 加载 列表 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();
            //1.查询数据集合
            var pageData = OperationContext.ServiceSession.RoleService.GetWithPagination(pageIndex, pageSize, o => o.roleIsDeleted == false, o => o.roleID, true);
            pageData.rows = pageData.rows.Select(o => o.ToPOCO()).ToList();
            //2.转成json格式字符串
            var jsSerializer = new JavaScriptSerializer();

            var strJson = jsSerializer.Serialize(pageData);
            return Content(strJson);
        }
        #endregion

        [HttpGet]
        /// <summary>
        /// 2.0 加载 设置角色权限 视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRolePermission(int id)
        {
            SetRolePermissionViewModel viewModel = new SetRolePermissionViewModel();
            viewModel.RoleID = id;
            //1.需要查出 角色 已经设置的 权限集合
            viewModel.AllPermissions = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionIsDeleted == false).ToList();
            //2.查询 所有的 权限集合 :
            //注意，EF生成连接查询： 1.直接使用Include方法 2.在EF的Select方法中 操作 实体类的导航属性
            viewModel.RolePermissions = OperationContext.ServiceSession.RolePermissionRelationshipService.Get(o => o.roleID == id).Select(o => o.Permission).ToList();
            //3.加载视图
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SetRolePermission()
        {
            //1.获取被设置权限的 角色id
            int roleId = Request.Form["rid"].AsInt();
            //2.获取新设置的 权限id集合
            List<int> newPermissionIdList = Request.Form["newPermissionIds"].Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.AsInt()).ToList();
            //3.查询角色 原有的权限
            var oldPerList = OperationContext.ServiceSession.RolePermissionRelationshipService.Get(o => o.roleID == roleId).ToList();
            //4.从集合中循环并删除 相同的新老权限对象，新集合中剩下的权限 就要 新增到数据库；旧集合中剩下的权限 就要从数据库中删除
            //4.1循环
            for (int i = oldPerList.Count - 1; i >= 0; i--)
            {
                var oldPer = oldPerList[i];
                if (newPermissionIdList.Contains(oldPer.permissionID))
                {
                    newPermissionIdList.Remove(oldPer.permissionID);
                    oldPerList.Remove(oldPer);
                }
            }
            //4.2新集合中剩下的权限 就要 新增到数据库
            newPermissionIdList.ForEach(newPerId =>
            {
                OperationContext.ServiceSession.RolePermissionRelationshipService.Add(new Model.RolePermissionRelationship() { roleID = roleId, permissionID = newPerId });
            });
            //4.3旧集合中剩下的权限 就要从数据库中删除
            oldPerList.ForEach(oldPer =>
            {
                OperationContext.ServiceSession.RolePermissionRelationshipService.Delete(oldPer);
            });
            OperationContext.ServiceSession.SaveChange();
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
        }

        #region 2.0 加载 新增 视图 + Add()
        /// <summary>
        /// 2.0 加载 新增 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            //1.准备 父权限 下拉框 o.perParent <= 1 &&
            var departmentId = OperationContext.ServiceSession.DepartmentService.Get(d => d.departmentIsDeleted==false , d => d.departmentID).ToList().Select(d => new SelectListItem()
            {
                Text = d.departmentName,
                Value = d.departmentID.ToString()
            });
            ViewBag.DepartmentId = departmentId;
            return View();
        }
        #endregion

        #region 2.1 保存 新增 数据 +Add(ViewModel.Permission viewModel)
        /// <summary>
        /// 2.1 保存 新增 数据
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
        #endregion



        #region 3.0 加载 修改 视图 +Modify(int id)
        /// <summary>
        /// 3.0 加载 修改 视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int id)
        {
            //1.查询要修改的 权限 实体对象
            var modifyData = OperationContext.ServiceSession.RoleService.Get(o => o.roleID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the role."); }

            //2.准备 父权限 下拉框
            var departmentId = OperationContext.ServiceSession.DepartmentService.Get(o => o.departmentIsDeleted == false , o => o.departmentID).ToList().Select(o => new SelectListItem()
            {
                Text = o.departmentName,
                Value = o.departmentID.ToString()
            });
            ViewBag.DepartmentId = departmentId;

            //3.将 实体对象 转成 视图模型对象 传给 视图
           // return View(modifyData.ToViewModel());
            return View(modifyData.ToViewModel());
        }
        #endregion

        [HttpPost]
        /// <summary>
        /// 3.1 保存 修改 数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult Modify(int id, RoleViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                //1.从url参数中 获取 要修改的 对象的 id
                viewmodel.roleID =id ;
                //2.修改权限
                OperationContext.ServiceSession.RoleService.Update(viewmodel.ToPOCO(), "roleName",  "roleIsDeleted");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }
    }
}
