using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.Repository;
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;
using Simon8029.EMPDemo.WebApp.Controllers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class SummaryController : BaseController
    {
        // GET: EmailMarketing/Summary
        [HttpGet]
        public ActionResult SummaryForSubmit(int id)
        {
            var campaignSummary = OperationContext.ServiceSession.EM_CampaignsService.Get(c => c.CampaignID == id).FirstOrDefault();
            
            return View(campaignSummary);
        }

        [HttpPost]
        public ActionResult SummaryForSubmit(int id, FormCollection form)
        {
            var campaign = OperationContext.ServiceSession.EM_CampaignsService.Get(c => c.CampaignID == id).FirstOrDefault();
            if (campaign != null)
            {
                campaign.Submitted = true;
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            else
            {
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "campaign is not found", "",
                    null);
            }
        }

        [HttpGet]
        public ActionResult SummaryForApprove(int id)
        {
            var campaignSummary = OperationContext.ServiceSession.EM_CampaignsService.Get(c => c.CampaignID == id).FirstOrDefault();
            var hasApproveCampaignPermission =
                OperationContext.CurrentUserPermissions.Where(p => p.permissionActionName == "ApproveCampaign")
                    .FirstOrDefault() != null;
            ViewBag.HasApproveCampaignPermission = hasApproveCampaignPermission;
            return View(campaignSummary);
        }
        [HttpPost]
        public ActionResult SummaryForApprove(int id, FormCollection form)
        {
           

            var campaign = OperationContext.ServiceSession.EM_CampaignsService.Get(c => c.CampaignID == id).FirstOrDefault();
            if (campaign != null)
            {
                campaign.Approved = true;
                campaign.ApprovedBy = OperationContext.CurrentUser.employeeLoginName;
                campaign.ApprovedDate=DateTime.Now;
                OperationContext.ServiceSession.SaveChange();

                //invoke stored procedure, populate data to campaignInstance.
                Entities entity = new Entities();
                var emailInstanceId =
                    OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.CampaignID == id)
                        .FirstOrDefault()
                        .EmailInstanceID;
                entity.EM_CampaignInstances_INSERT(emailInstanceId);

                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            else
            {
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "campaign is not found", "",
                    null);
            }
        }
    }
}