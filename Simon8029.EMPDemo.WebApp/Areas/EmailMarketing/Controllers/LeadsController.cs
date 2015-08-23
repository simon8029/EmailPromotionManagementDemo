using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Simon8029.EMPDemo.WebApp.Controllers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class LeadsController : BaseController
    {
        
        public ActionResult Index()
        {
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();
            int totalCount = 10;
            var leadsList = OperationContext.ServiceSession.EM_LeadsService.GetWithPagination(pageIndex, pageSize, o => true, o => o.LeadID, true);
            leadsList.rows = leadsList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(leadsList));
        }

        
    }
}