using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models
{


    public class SetRolePermissionViewModel
    {
       
        public int RoleID;
       
        public List<Permission> AllPermissions;
        
        public List<Permission> RolePermissions;
    }
}
