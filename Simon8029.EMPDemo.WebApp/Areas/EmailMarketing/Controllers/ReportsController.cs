using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Simon8029.EMPDemo.WebApp.Controllers;
using Point = System.Drawing.Point;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: EmailMarketing/Reports
        [HttpGet]
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

            //已发送邮件的campaignInstances（有重复值）
            var CampaignInstances_isSent =
                OperationContext.ServiceSession.EM_CampaignInstancesService.Get(c => c.IsSent).ToList();

            //已发送的邮件Id列表（去掉重复值）
            var emailInstanceId_isSent = new List<int>();
            foreach (var campaignInstance in CampaignInstances_isSent)
            {
                if (!emailInstanceId_isSent.Contains(campaignInstance.EmailInstanceID))
                {
                    emailInstanceId_isSent.Add(campaignInstance.EmailInstanceID);
                }
            }

            //已发送邮件的emailInstance集合
            var emailInstances_isSent =
                OperationContext.ServiceSession.EM_EmailInstancesService.Get(
                    e => emailInstanceId_isSent.Contains(e.EmailInstanceID)).ToList();

            //已发送邮件的campaignIds
            List<int> campaignIds_isSent = new List<int>();
            foreach (var emailInstance in emailInstances_isSent)
            {
                campaignIds_isSent.Add(emailInstance.CampaignID);
            }

            //已发送邮件的campaign集合
            var campaignList = OperationContext.ServiceSession.EM_CampaignsService.GetWithPagination(pageIndex, pageSize,
                c => campaignIds_isSent.Contains(c.CampaignID), c => c.CampaignID, true);
            campaignList.rows = campaignList.rows.Select(r => r.ToPOCO()).ToList();
            var javascriptSerializer = new JavaScriptSerializer();
            return Content(javascriptSerializer.Serialize(campaignList));
        }

        public ActionResult ShowChart(int id)
        {
            //1. 获取已发送邮件的campaignInstance总数
            //1.1 获取已发邮件的邮件id
            var emailId =
                OperationContext.ServiceSession.EM_EmailInstancesService.Get(e => e.CampaignID == id)
                    .FirstOrDefault()
                    .EmailInstanceID;
            //1.2 获取已发邮件的campaign总数
            var totalCampaigns =
                OperationContext.ServiceSession.EM_CampaignInstancesService.Get(c => c.EmailInstanceID == emailId)
                    .Count();

            //2. 获取已读邮件的campaignInstance总数
            var openedCampaign =
                OperationContext.ServiceSession.EM_CampaignInstancesService.Get(
                    c => c.EmailInstanceID == emailId && c.EventStatus == "Y").Count();
            //3. 计算百分比
            var openedRatePercentage = Math.Round(openedCampaign * 1.0 / totalCampaigns * 100, 2);
            var unOpenedRatePercentage = Math.Round(((totalCampaigns - openedCampaign) * 1.0 / totalCampaigns) * 100, 2);

            //设置饼图
            Highcharts chart = new Highcharts("PieChart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "Open Rate untill: "+DateTime.Now })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetCredits(new Credits(){Enabled = false})
                .SetPlotOptions(new PlotOptions
                {

                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {

                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {

                    Type = ChartTypes.Pie,
                    Name = "Browser share",
                    Data = new Data(new object[]
                                               {
                                                   new object[] { "opened", openedRatePercentage },
                                                   new object[] { "unopened", unOpenedRatePercentage }
                                               })
                });

            return View(chart);
            
        }
    }
}