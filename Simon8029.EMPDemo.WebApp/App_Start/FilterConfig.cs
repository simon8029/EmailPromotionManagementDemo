using System.Web;
using System.Web.Mvc;
using Simon8029.EMPDemo.WebApp.Filters;

namespace Simon8029.EMPDemo.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CheckPermissionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
