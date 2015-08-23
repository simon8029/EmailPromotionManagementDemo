using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;

namespace Simon8029.EMPDemo.Model
{
    public partial class Permission
    {
        /// <summary>
        /// 将 权限 转成 节点对象
        /// </summary>
        /// <returns></returns>
        public EasyUIModel_MenuTreeNode ToMenuTreeNode()
        {
            return new EasyUIModel_MenuTreeNode()
            {
                id = this.permissionID,
                text = this.permissionName,
                attributes = new
                {
                    url = "/" + this.permissionAreaName + "/" + this.permissionControllerName + "/" + this.permissionActionName,
                    isLink = this.permissionIsLink
                },
                @checked = false,
                state = "open"
            };
        }

    }
}
