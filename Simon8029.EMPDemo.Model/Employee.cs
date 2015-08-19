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
    
    public partial class Employee
    {
        public Employee()
        {
            this.employeeRoleRelationship = new HashSet<employeeRoleRelationship>();
            this.VipPermission = new HashSet<VipPermission>();
        } 
    
        public int employeeID { get; set; }
        public Nullable<int> employeeDepartmentID { get; set; }
        public string employeeCnName { get; set; }
        public string employeeLoginName { get; set; }
        public string employeeLoginPassword { get; set; }
        public bool employeeGender { get; set; }
        public string employeePhone { get; set; }
        public string employeeAddress { get; set; }
        public bool employeeIsDeleted { get; set; }
        public System.DateTime employeeAddTime { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<employeeRoleRelationship> employeeRoleRelationship { get; set; }
        public virtual ICollection<VipPermission> VipPermission { get; set; }
    }
}