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



      
        [HttpGet]
        public ActionResult Modify(int id)
        {
            var modifyData = OperationContext.ServiceSession.EM_CampaignsService.Get(l => l.CampaignID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the campaign."); }

            return View(modifyData.ToViewModel());
        }

        [HttpPost]
     
        public ActionResult Modify(int id, CampaignViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.CampaignID = id;
                OperationContext.ServiceSession.EM_CampaignsService.Update(viewmodel.ToPOCO(), "CampaignDesc", "CampaignName", "UpdatedBy", "UpdatedDate", "StartDate", "EndDate");
                OperationContext.ServiceSession.SaveChange();

   
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