using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models
{
    public class EmailViewModel
    {
        public int EmailInstanceID { get; set; }
        public int CampaignID { get; set; }
        public string SubjectLine { get; set; }
        public string EmailBody { get; set; }
        public Nullable<byte> Step { get; set; }
        public Nullable<byte> PreviousStep { get; set; }
        public bool EnableTracking { get; set; }
        public bool IsDraft { get; set; }
        public int Timespan { get; set; }
        public Nullable<System.DateTime> AbsoluteDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public EM_EmailInstances ToPOCO()
        {
            var operationContext = new OperationContext();
            return new EM_EmailInstances()
            {
                EmailInstanceID = EmailInstanceID,
                CampaignID = CampaignID,
                SubjectLine = SubjectLine,
                EmailBody = EmailBody,
                Step = Step,
                PreviousStep = PreviousStep,
                EnableTracking = EnableTracking,
                IsDraft = true,
                Timespan = Timespan,
                AbsoluteDate = AbsoluteDate,
                CreatedDate = DateTime.Now,
                CreatedBy = operationContext.CurrentUser.employeeLoginName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = operationContext.CurrentUser.employeeLoginName
            };
        }
    }
}