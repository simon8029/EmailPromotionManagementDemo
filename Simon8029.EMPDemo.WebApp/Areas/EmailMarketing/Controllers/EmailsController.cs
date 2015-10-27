using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class EmailsController : BaseController
    {
        // GET: EmailMarketing/Emails
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
            var leadsList = OperationContext.ServiceSession.EM_EmailInstancesService.GetWithPagination(pageIndex, pageSize, o => true, o => o.CampaignID, true);
            leadsList.rows = leadsList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(leadsList));
        }

     
        [HttpGet]
        public ActionResult Add(int id)
        {
            var emailInstance = new EM_EmailInstances();
            emailInstance.CampaignID = id;
            return View(emailInstance.ToViewModel());
        }

     
        [HttpPost]
        public ActionResult Add(EmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.EM_EmailInstancesService.Add(viewModel.ToPOCO());
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
            var email = OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.EmailInstanceID == id).SingleOrDefault();
            if (email == null)
            {
                email = new EM_EmailInstances();
                email.CampaignID = id;
            }

            return View(email.ToViewModel());
        }

        [HttpPost]
    
        public ActionResult Modify(int id, EmailViewModel emailViewmodel)
        {
            if (ModelState.IsValid)
            {
                emailViewmodel.EmailInstanceID = id;

                OperationContext.ServiceSession.EM_EmailInstancesService.Update(emailViewmodel.ToPOCO(), "SubjectLine", "EmailBody", "EnableTracking", "IsDraft", "Timespan", "AbsoluteDate", "UpdatedDate", "UpdatedBy");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }

        //public string GetCurrentUserName()
        //{
        //    return OperationContext.CurrentUser.employeeLoginName;
        //}
    }
}