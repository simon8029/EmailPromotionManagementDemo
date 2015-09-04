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
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class CampaignsController : BaseController
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
            var campaignList = OperationContext.ServiceSession.EM_CampaignsService.GetWithPagination(pageIndex, pageSize, o => true, o => o.CampaignID, true);
            campaignList.rows = campaignList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(campaignList));
        }

        #region 2.0 加载 新增 视图 + Add()
        /// <summary>
        /// 2.0 加载 新增 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            var campaignViewModel = new CampaignViewModel();
            var campaignWithMaxId = OperationContext.ServiceSession.EM_CampaignsService.Get(c => true).OrderByDescending(c => c.CampaignID).FirstOrDefault();
            if (campaignWithMaxId != null)
            {
                campaignViewModel.CampaignID = campaignWithMaxId.CampaignID + 1;
            }
            else
            {
                campaignViewModel.CampaignID = 1;
            }
            return View(campaignViewModel);
        }
        #endregion

        #region 2.1 保存 新增 数据 +Add(models.l viewModel)
        /// <summary>
        /// 2.1 保存 新增 数据
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CampaignViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.EM_CampaignsService.Add(viewModel.ToPOCO());
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
            var modifyData = OperationContext.ServiceSession.EM_CampaignsService.Get(l => l.CampaignID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the campaign."); }

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
        public ActionResult Modify(int id, CampaignViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                //1.从url参数中 获取 要修改的 对象的 id
                viewmodel.CampaignID = id;
                //2.修改权限
                OperationContext.ServiceSession.EM_CampaignsService.Update(viewmodel.ToPOCO(), "CampaignDesc", "CampaignName", "UpdatedBy", "UpdatedDate", "StartDate", "EndDate");
                OperationContext.ServiceSession.SaveChange();

                //3.从emailInstance表中获取当前campaign的email信息，若已有email则在sendajaxmessage中将Message设置为hasEmail,并将该emailId赋给viewbag
                string hasEmail = string.Empty;
                string emailInstanceId=string.Empty;
                if (OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.CampaignID == id).FirstOrDefault() != null)
                {
                    hasEmail = "hasEmail";
                    emailInstanceId =
                        OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.CampaignID == id)
                            .FirstOrDefault()
                            .EmailInstanceID.ToString();
                    //ViewBag.emailInstanceId = emailInstanceId;
                }
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, hasEmail, "",emailInstanceId);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }

        //public string GetCurrentUserName()
        //{
        //    return OperationContext.CurrentUser.employeeLoginName;
        //}

        public bool HasEmail(int id)
        {
            return
                OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.CampaignID == id).FirstOrDefault() !=
                null;
        }
        [HttpPost]
        public ActionResult SummaryForApprove(int id)
        {
            return null;
        }
    }
}