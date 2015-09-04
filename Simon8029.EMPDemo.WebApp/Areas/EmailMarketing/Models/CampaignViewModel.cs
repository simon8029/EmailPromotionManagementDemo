using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models
{
    public class CampaignViewModel
    {
        public int CampaignID { get; set; }
        [DisplayName("Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DisplayName("End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DisplayName("Campaign Name"),Required]
        public string CampaignName { get; set; }
        [DisplayName("Campaign Description"),DataType(DataType.MultilineText)]
        public string CampaignDesc { get; set; }
        [DisplayName("Owner")]
        public string Owner { get; set; }
        [DisplayName("Approval Request"),Required]
        public bool ApprovalRequest { get; set; }
        [DisplayName("Approved"), Required]
        public bool Approved { get; set; }
        [DisplayName("Approved By")]
        public string ApprovedBy { get; set; }
        [DisplayName("Approved Date")]
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public System.Guid Guid { get; set; }
        [DisplayName("Submitted"),Required]
        public bool Submitted { get; set; }
        [DisplayName("Created Date"), Required]
        public System.DateTime CreatedDate { get; set; }
        [DisplayName("Created By"), Required]
        public string CreatedBy { get; set; }
        [DisplayName("Updated Date")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DisplayName("Updated By")]
        public string UpdatedBy { get; set; }


        public EM_Campaigns ToPOCO()
        {
            var operationContext = new OperationContext();
            return new EM_Campaigns()
            {
                CampaignID = CampaignID,
                ApprovalRequest = false,
                Approved = false,
                ApprovedBy = ApprovedBy,
                ApprovedDate = ApprovedDate,
                CampaignDesc = CampaignDesc,
                CampaignName = CampaignName,
                CreatedBy = operationContext.CurrentUser.employeeLoginName,
                CreatedDate = DateTime.Now,
                UpdatedBy = operationContext.CurrentUser.employeeLoginName,
                UpdatedDate = DateTime.Now,
                StartDate = StartDate,
                EndDate = EndDate,
                Owner = Owner,
                Submitted = false,
                Guid = Guid.NewGuid()
            };
        }
    }


}