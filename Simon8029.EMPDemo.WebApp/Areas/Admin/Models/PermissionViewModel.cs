using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models
{

    /// <summary>
    /// 权限 视图模型
    /// </summary>
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }

        [DisplayName("权限名称"), Required(ErrorMessage = "不能空")]
        public string PermissionName { get; set; }
        [DisplayName("父权限")]
        public int PermissionParentId { get; set; }
        [DisplayName("备注"), Required(ErrorMessage = "不能空")]
        public string PermissionReMark { get; set; }
        [DisplayName("区域名"), Required(ErrorMessage = "不能空")]
        public string PermissionAreaName { get; set; }
        [DisplayName("控制器名"), Required(ErrorMessage = "不能空")]
        public string PermissionControllerName { get; set; }
        [DisplayName("Action方法名"), Required(ErrorMessage = "不能空")]
        public string PermissionActionName { get; set; }
        [DisplayName("请求方式")]
        public short PermissionFormMethod { get; set; }
        [DisplayName("操作类型")]
        public short PermissionOperationType { get; set; }
        [DisplayName("按钮JS方法名")]
        public string PermissionJsMethodName { get; set; }
        [DisplayName("是否为链接")]
        public bool PermissionIsLink { get; set; }
        [DisplayName("是否显示")]
        public bool PermissionIsShow { get; set; }
        [DisplayName("序号"), Required(ErrorMessage = "不能空")]
        public int PermissionOrder { get; set; }
        [DisplayName("图标"), Required(ErrorMessage = "不能空")]
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
