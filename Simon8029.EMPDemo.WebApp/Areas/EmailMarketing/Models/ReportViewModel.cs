using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models
{
    public class ReportViewModel
    {
        public int CampaignID { get; set; }
        [DisplayName("Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DisplayName("End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DisplayName("Campaign Name"), Required]
        public string CampaignName { get; set; }
        [DisplayName("Campaign Description"), DataType(DataType.MultilineText)]
        public string CampaignDesc { get; set; }

        public int OpenedEmailPercentage { get; set; }
        public int UnOpenedEmailPercentage { get; set; }

        public EM_Campaigns ToPOCO()
        {
            var operationContext = new OperationContext();
            return new EM_Campaigns()
            {
                CampaignID = CampaignID,
                ApprovalRequest = false,
                Approved = false,
                CampaignDesc = CampaignDesc,
                CampaignName = CampaignName,
                CreatedBy = operationContext.CurrentUser.employeeLoginName,
                CreatedDate = DateTime.Now,
                UpdatedBy = operationContext.CurrentUser.employeeLoginName,
                UpdatedDate = DateTime.Now,
                StartDate = StartDate,
                EndDate = EndDate,
                Submitted = false,
                Guid = Guid.NewGuid()
            };
        }
    }
}