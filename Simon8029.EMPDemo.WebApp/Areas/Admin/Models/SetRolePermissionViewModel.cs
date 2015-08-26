using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models
{

    /// <summary>
    /// 为角色 分配 权限 视图模型
    /// </summary>
    public class SetRolePermissionViewModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID;
        /// <summary>
        /// 所有权限集合
        /// </summary>
        public List<Permission> AllPermissions;
        /// <summary>
        /// 角色权限集合
        /// </summary>
        public List<Permission> RolePermissions;
    }
}
