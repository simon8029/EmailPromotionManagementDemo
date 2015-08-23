using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.IService;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.Service
{
    public partial class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public List<Permission> GetUserPermissions(int userId)
        {
            var roleIds = DbSession.employeeRoleRelationshipRepository.Get(r => r.employeeID == userId).Select(r => r.RoleID).ToList();
            var permissionIds = DbSession.RolePermissionRelationshipRepository.Get(p => roleIds.Contains(p.roleID)).Select(p => p.permissionID).ToList();
            var vipPermissionIds = DbSession.VipPermissionRepository.Get(p => p.userID == userId).Select(p => p.permissionID).ToList();
            vipPermissionIds.ForEach(vipPermissionId =>
            {
                if (!permissionIds.Contains(vipPermissionId))
                {
                    permissionIds.Add(vipPermissionId);
                }
            });

            var permissions = DbSession.PermissionRepository.Get(p => permissionIds.Contains(p.permissionID))
                    .ToList()
                    .Select(p => p.ToPOCO())
                    .OrderBy(p => p.permissionOrder)
                    .ToList();
            return permissions;
        }
    }
}
