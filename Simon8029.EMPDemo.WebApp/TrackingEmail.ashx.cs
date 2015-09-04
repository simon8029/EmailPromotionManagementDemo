using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Service;

namespace Simon8029.EMPDemo.WebApp
{
    /// <summary>
    /// Summary description for TrackingEmail
    /// </summary>
    public class TrackingEmail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //从url中获取campaignInstance的guid
            try
            {
                string url = context.Request.RawUrl;
                string id = context.Request.QueryString["id"];
                EM_CampaignsService emCampaignsService = new EM_CampaignsService();
                emCampaignsService.UpdateCampaignInstanceEvent(id, 1, "Y", DateTime.Now);
            }
            catch (Exception)
            {
                //Should keep application running
                throw;
            }

            //向客户端返回图片
            context.Response.ContentType = "image/jpeg";
            context.Response.TransmitFile(HttpContext.Current.Server.MapPath("/images/triangle.jpg"));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}