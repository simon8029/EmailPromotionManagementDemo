//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Simon8029.EMPDemo.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EM_Campaigns
    {
        public EM_Campaigns()
        {
            this.EM_EmailInstances = new HashSet<EM_EmailInstances>();
        }
    
        public int CampaignID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDesc { get; set; }
        public string Owner { get; set; }
        public Nullable<bool> ApprovalRequest { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public System.Guid Guid { get; set; }
        public Nullable<bool> Submitted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<EM_EmailInstances> EM_EmailInstances { get; set; }
    }
}
