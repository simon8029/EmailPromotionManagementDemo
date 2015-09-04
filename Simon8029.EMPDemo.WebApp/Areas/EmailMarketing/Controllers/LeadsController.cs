using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models;
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class LeadsController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();
            int totalCount = 10;
            var leadsList = OperationContext.ServiceSession.EM_LeadsService.GetWithPagination(pageIndex, pageSize, o => true, o => o.LeadID, true);
            leadsList.rows = leadsList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(leadsList));
        }

        #region 2.0 加载 新增 视图 + Add()
        /// <summary>
        /// 2.0 加载 新增 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        #endregion

        #region 2.1 保存 新增 数据 +Add(models.l viewModel)
        /// <summary>
        /// 2.1 保存 新增 数据
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(LeadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.EM_LeadsService.Add(viewModel.ToPOCO());
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
            var modifyData = OperationContext.ServiceSession.EM_LeadsService.Get(l => l.LeadID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the lead."); }

            //3.将 实体对象 转成 视图模型对象 传给 视图
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
        public ActionResult Modify(int id, LeadViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                //1.从url参数中 获取 要修改的 对象的 id
                viewmodel.LeadID = id;
                //2.修改权限
                OperationContext.ServiceSession.EM_LeadsService.Update(viewmodel.ToPOCO(), "LeadID","FirstName","LastName","EmailAddress","IsValid","Unsubscribed");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }
        
    }
}