using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models;
using Simon8029.EMPDemo.WebApp.Controllers;
using Simon8029.EMPDemo.WebApp.Helpers;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class EmailsController : BaseController
    {
        // GET: EmailMarketing/Emails
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            int pageIndex = Request.Form["page"].AsInt();
            int pageSize = Request.Form["rows"].AsInt();
            int totalCount = 10;
            var leadsList = OperationContext.ServiceSession.EM_EmailInstancesService.GetWithPagination(pageIndex, pageSize, o => true, o => o.CampaignID, true);
            leadsList.rows = leadsList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(leadsList));
        }

        #region 2.0 加载 新增 视图 + Add()
        /// <summary>
        /// 2.0 加载 新增 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add(int id)
        {
            var emailInstance = new EM_EmailInstances();
            emailInstance.CampaignID = id;
            return View(emailInstance.ToViewModel());
        }
        #endregion

        #region 2.1 保存 新增 数据 +Add(models.l viewModel)
        /// <summary>
        /// 2.1 保存 新增 数据
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                OperationContext.ServiceSession.EM_EmailInstancesService.Add(viewModel.ToPOCO());
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            else
            {
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in browser.", "", null);
            }
        }
        #endregion



        #region 3.0 加载 修改 视图 +Modify(int id)
        /// <summary>
        /// 3.0 加载 修改 视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int id)
        {
            //1.查询要修改的 权限 实体对象
            var email = OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.EmailInstanceID == id).SingleOrDefault();
            if (email == null)//若当前campaign尚无email，则新建一个空email，设置期campaignID为当前campaignID，便于用户点击返回按钮时可显示当前campaign
            {
                email = new EM_EmailInstances();
                email.CampaignID = id;
            }

            //3.将 实体对象 转成 视图模型对象 传给 视图
            return View(email.ToViewModel());
        }
        #endregion

        [HttpPost]
        /// <summary>
        /// 3.1 保存 修改 数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult Modify(int id, EmailViewModel emailViewmodel)
        {
            if (ModelState.IsValid)
            {
                ////1.将url参数中传入的emaiInstanceId赋给emailInstance。
                emailViewmodel.EmailInstanceID = id;

                //2.修改权限
                OperationContext.ServiceSession.EM_EmailInstancesService.Update(emailViewmodel.ToPOCO(), "SubjectLine", "EmailBody", "EnableTracking", "IsDraft", "Timespan", "AbsoluteDate", "UpdatedDate", "UpdatedBy");
                OperationContext.ServiceSession.SaveChange();
                return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationSuccess, "", "", null);
            }
            return OperationContext.SendAjaxMessage(AjaxMessageStatus.OperationFailed, "Please enable javascript in the browser", "", null);
        }

        //public string GetCurrentUserName()
        //{
        //    return OperationContext.CurrentUser.employeeLoginName;
        //}
    }
}