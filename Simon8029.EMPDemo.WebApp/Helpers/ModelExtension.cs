using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models;

namespace Simon8029.EMPDemo.WebApp.Helpers
{
    public static class ModelExtension
    {
        /// <summary>
        /// 将 权限实体 对象 转成 权限视图模型 对象
        /// </summary>
        /// <param name="permissionpermissionPOCO"></param>
        /// <returns></returns>
        public static PermissionViewModel ToViewModel(this Model.Permission permissionPOCO)
        {
            var permissionViewModel = new PermissionViewModel();
            
            return new PermissionViewModel()
            {
                PermissionId =  permissionPOCO.permissionID,
                PermissionParentId = permissionPOCO.permissionParentID,
                PermissionName = permissionPOCO.permissionName,
                PermissionReMark = permissionPOCO.permissionRemark,
                PermissionAreaName = permissionPOCO.permissionAreaName,
                PermissionControllerName = permissionPOCO.permissionControllerName,
                PermissionActionName = permissionPOCO.permissionActionName,
                PermissionFormMethod = permissionPOCO.permissionFormMethod,
                PermissionOperationType = permissionPOCO.permissionOperationType,
                PermissionJsMethodName = permissionPOCO.permissionJSMethodName,
                PermissionIco = permissionPOCO.permissionIcon,
                PermissionIsLink = permissionPOCO.permissionIsLink,
                PermissionOrder = permissionPOCO.permissionOrder,
                PermissionIsShow = permissionPOCO.permissionIsShow
            };
        }
    }
}