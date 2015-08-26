using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public int roleID { get; set; }
        [DisplayName("Role Name"),Required]
        public string roleName { get; set; }

        public Role ToPOCO()
        {
            return new Role()
            {
                roleID = this.roleID,
                roleName = this.roleName,
                roleIsDeleted = false,
                roleAddTime = DateTime.Now
            };
        }
    }
}