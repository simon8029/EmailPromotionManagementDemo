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

  
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        

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

       

       
        [HttpGet]
        public ActionResult Modify(int id)
        {
            var modifyData = OperationContext.ServiceSession.EM_LeadsService.Get(l => l.LeadID == id).SingleOrDefault();
            if (modifyData == null) { throw new Exception("Can not find the lead."); }

            return View(modifyData.ToViewModel());
        }

        [HttpPost]
       
        public ActionResult Modify(int id, LeadViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.LeadID = id;
                OperationContext.ServiceSession.EM_LeadsService.Update(viewmodel.ToPOCO(), "LeadID","FirstName","LastName","EmailAddress","IsValid","Unsubscribed");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }
        
    }
}