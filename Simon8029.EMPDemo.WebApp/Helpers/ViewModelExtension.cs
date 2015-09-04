using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.WebApp.Areas.Admin.Models;
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;

namespace Simon8029.EMPDemo.WebApp.Helpers
{
    public static class ViewModelExtension
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

        public static RoleViewModel ToViewModel(this Model.Role rolePOCO)
        {
            var roleViewModel = new RoleViewModel();

            return new RoleViewModel()
            {
                roleID = rolePOCO.roleID,
                roleName = rolePOCO.roleName,
            };
        }

        public static LeadViewModel ToViewModel(this EM_Leads leadPOCO)
        {
            return new LeadViewModel()
            {
                LeadID = leadPOCO.LeadID,
                FirstName = leadPOCO.FirstName,
                LastName = leadPOCO.LastName,
                EmailAddress = leadPOCO.EmailAddress,
                IsValid = leadPOCO.IsValid,
                Unsubscribed = leadPOCO.Unsubscribed
            };
        }

        public static CampaignViewModel ToViewModel(this EM_Campaigns campaignPOCO)
        {
            return new CampaignViewModel()
            {
                CampaignID = campaignPOCO.CampaignID,
                ApprovalRequest = campaignPOCO.ApprovalRequest,
                Approved = campaignPOCO.Approved,
                ApprovedBy = campaignPOCO.ApprovedBy,
                ApprovedDate = campaignPOCO.ApprovedDate,
                CampaignDesc = campaignPOCO.CampaignDesc,
                CampaignName = campaignPOCO.CampaignName,
                CreatedBy = campaignPOCO.CreatedBy,
                CreatedDate = campaignPOCO.CreatedDate,
                UpdatedBy = campaignPOCO.UpdatedBy,
                UpdatedDate = campaignPOCO.UpdatedDate,
                StartDate = campaignPOCO.StartDate,
                EndDate = campaignPOCO.EndDate,
                Owner = campaignPOCO.Owner,
                Submitted = campaignPOCO.Submitted,
                Guid = campaignPOCO.Guid
            };
        }

        public static EmailViewModel ToViewModel(this EM_EmailInstances emailPOCO)
        {
            return new EmailViewModel()
            {
                EmailInstanceID = emailPOCO.EmailInstanceID,
                CampaignID = emailPOCO.CampaignID,
                SubjectLine = emailPOCO.SubjectLine,
                EmailBody = emailPOCO.EmailBody,
                Step = emailPOCO.Step,
                PreviousStep = emailPOCO.PreviousStep,
                EnableTracking = emailPOCO.EnableTracking,
                IsDraft = emailPOCO.IsDraft,
                Timespan = emailPOCO.Timespan,
                AbsoluteDate = emailPOCO.AbsoluteDate,
                CreatedDate = emailPOCO.CreatedDate,
                CreatedBy = emailPOCO.CreatedBy,
                UpdatedDate = emailPOCO.UpdatedDate,
                UpdatedBy = emailPOCO.UpdatedBy
            };
        }
    }
}