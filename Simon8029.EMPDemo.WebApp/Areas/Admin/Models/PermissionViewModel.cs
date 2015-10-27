using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models
{


    public class PermissionViewModel
    {
        public int PermissionId { get; set; }

        [DisplayName("Permission Name"), Required]
        public string PermissionName { get; set; }
        [DisplayName("Parent Permission")]
        public int PermissionParentId { get; set; }
        [DisplayName("Remark"), Required]
        public string PermissionReMark { get; set; }
        [DisplayName("Area Name"), Required]
        public string PermissionAreaName { get; set; }
        [DisplayName("Controller Name"), Required]
        public string PermissionControllerName { get; set; }
        [DisplayName("Action Name"), Required]
        public string PermissionActionName { get; set; }
        [DisplayName("Form Method")]
        public short PermissionFormMethod { get; set; }
        [DisplayName("Operation Type")]
        public short PermissionOperationType { get; set; }
        [DisplayName("JS Method Name")]
        public string PermissionJsMethodName { get; set; }
        [DisplayName("Is Link")]
        public bool PermissionIsLink { get; set; }
        [DisplayName("Is Display")]
        public bool PermissionIsShow { get; set; }
        [DisplayName("Order Number"), Required]
        public int PermissionOrder { get; set; }
        [DisplayName("Icon"), Required]
        public string PermissionIco { get; set; }

        /// <summary>
        /// 将视图模型 转成 对应的实体模型
        /// </summary>
        /// <returns></returns>
        public Model.Permission ToPOCO()
        {
            return new Model.Permission()
            {
                permissionID = this.PermissionId,
                permissionParentID = this.PermissionParentId,
                permissionName = this.PermissionName,
                permissionRemark = this.PermissionReMark,
                permissionAreaName = this.PermissionAreaName,
                permissionControllerName = this.PermissionControllerName,
                permissionActionName = this.PermissionActionName,
                permissionFormMethod = this.PermissionFormMethod,
                permissionOperationType = this.PermissionOperationType,
                permissionJSMethodName = this.PermissionJsMethodName,
                permissionIcon = this.PermissionIco,
                permissionIsLink = this.PermissionIsLink,
                permissionOrder = this.PermissionOrder,
                permissionIsShow = this.PermissionIsShow,
                permissionIsDeleted = false,
                permissionAddTime = DateTime.Now
            };
        }

        ///// <summary>
        ///// 将 权限实体模型 转成 权限视图模型
        ///// </summary>
        ///// <param name="poco"></param>
        ///// <returns></returns>
        //public static ViewModel.Permission ToViewModel(MODEL.Permission poco)
        //{
        //    return new ViewModel.Permission() {
        //        perId = poco.perId,
        //        perParent = poco.perParent,
        //        perName = poco.perName,
        //        perRemark = poco.perRemark,
        //        perAreaName = poco.perAreaName,
        //        perControllerName = poco.perControllerName,
        //        perActionName = poco.perActionName,
        //        perFormMethod = poco.perFormMethod,
        //        perOperationType = poco.perOperationType,
        //        perJsMethodName = poco.perJsMethodName,
        //        perIco = poco.perIco,
        //        perIsLink = poco.perIsLink,
        //        perOrder = poco.perOrder,
        //        perIsShow = poco.perIsShow
        //    };
        //}
    }
}
