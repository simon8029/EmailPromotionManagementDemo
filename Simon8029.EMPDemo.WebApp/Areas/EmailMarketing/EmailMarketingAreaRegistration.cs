using System.Web.Mvc;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing
{
    public class EmailMarketingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmailMarketing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmailMarketing_default",
                "EmailMarketing/{controller}/{action}/{id}",
                new { controller = "Leads", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}