//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Simon8029.EMPDemo.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()  
        {
            this.RolePermissionRelationship = new HashSet<RolePermissionRelationship>();
            this.VipPermission = new HashSet<VipPermission>();
        }
    
        public int permissionID { get; set; }
        public int permissionParentID { get; set; }
        public string permissionName { get; set; }
        public string permissionRemark { get; set; }
        public string permissionAreaName { get; set; }
        public string permissionControllerName { get; set; }
        public string permissionActionName { get; set; }
        public short permissionFormMethod { get; set; }
        public short permissionOperationType { get; set; }
        public string permissionJSMethodName { get; set; }
        public string permissionIcon { get; set; }
        public bool permissionIsLink { get; set; }
        public int permissionOrder { get; set; }
        public bool permissionIsShow { get; set; }
        public bool permissionIsDeleted { get; set; }
        public System.DateTime permissionAddTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermissionRelationship> RolePermissionRelationship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VipPermission> VipPermission { get; set; }
    }
}
