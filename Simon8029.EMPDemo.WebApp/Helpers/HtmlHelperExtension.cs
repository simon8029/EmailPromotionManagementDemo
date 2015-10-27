using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Utilities;
namespace Simon8029.EMPDemo.WebApp
{
    public static class HtmlHelperExtension
    {
         #region 1.0  Get current user's buttons
        public static System.Web.Mvc.MvcHtmlString GetButtonsForToolbar(this System.Web.Mvc.HtmlHelper htmlHelper)
        {

            StringBuilder sbBtnJs = new StringBuilder(1000);
            foreach (var button in htmlHelper.ViewBag.toolbarButtons as List<Permission>)
            {
                sbBtnJs.Append("{");
                sbBtnJs.Append("iconCls:'" + button.permissionIcon + "',");
                sbBtnJs.Append("text:'" + button.permissionName+ "',");
                sbBtnJs.Append("handler:" + button.permissionJSMethodName + "");
                sbBtnJs.Append("},'-',");
            }
            System.Web.Mvc.MvcHtmlString mvcStr = new System.Web.Mvc.MvcHtmlString(sbBtnJs.ToString());
            return mvcStr;
        } 
        #endregion

        #region 2.0 detect user's button 
        public static bool IsButtonExist(this System.Web.Mvc.HtmlHelper htmlHelper, string strJsMethodName)
        {
            var btns = htmlHelper.ViewBag.toolbarButtons;
            var buttons = (htmlHelper.ViewBag.toolbarButtons as List<Permission>).FirstOrDefault(o => o.permissionJSMethodName.IsSame(strJsMethodName));
            var isButtonExist = buttons != null;
            return isButtonExist;
        } 
        #endregion
    }
    
}
