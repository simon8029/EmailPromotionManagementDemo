using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Service;
using Simon8029.EMPDemo.WebApp;

namespace Simon8029.EMPDemo.DeliveryEmail
{
    internal class Program
    {
        private static string sendEmailFrom = "";

        private static void Main(string[] args)
        {
            //设置发送邮件地址
            sendEmailFrom = ConfigurationManager.AppSettings["sendEmailFrom"];

            //通过campaignInstance表中的条件为未发送的记录，获取emailInstanceId，进而用其在EmailInstance表中获取campaignId，然后再在campaign表中获取campaign的开始结束时间，用来判断是否需要马上发送。
            var operationContext = new OperationContext();
            var campaignInstances =
                operationContext.ServiceSession.EM_CampaignInstancesService.Get(c => c.IsSent == false);
            foreach (var campaignInstance in campaignInstances)
            {
                var emailInstanceId = campaignInstance.EmailInstanceID;
                var campaignId =
                    operationContext.ServiceSession.EM_EmailInstancesService.Get(
                        e => e.EmailInstanceID == emailInstanceId).FirstOrDefault().CampaignID;
                var campaign =
                    operationContext.ServiceSession.EM_CampaignsService.Get(c => c.CampaignID == campaignId)
                        .FirstOrDefault();
                if (campaign.StartDate.Value < DateTime.Now && campaign.EndDate.Value > DateTime.Now)
                {
                    //SendEmail(campaignInstance);
                    //设置待发邮件
                    MailMessage message = new MailMessage();
                    var email = campaignInstance.EM_EmailInstances;
                    message.Subject = email.SubjectLine;
                    message.Body = email.EmailBody;
                    message.From = new MailAddress(sendEmailFrom);
                    message.ReplyToList.Add(new MailAddress(sendEmailFrom));
                    message.IsBodyHtml = true;

                    //向邮件中添加客户信息
                    string name = campaignInstance.EM_Leads.FirstName;
                    message.To.Add(new MailAddress(campaignInstance.EM_Leads.EmailAddress));

                    //发送邮件
                    EmailHelper.SendEmail(message);

                    //将该CampaignInstance标注为已发送
                    campaignInstance.IsSent = true;
                    operationContext.ServiceSession.SaveChange();


                }

            }

            //private static void SendEmail(EM_CampaignInstances campaignInstance)
            //{

            //}
        }
    }
}
