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
         #region 1.0 获取 当前登录用户 拥有的受访页面的 按钮
        /// <summary>
        /// 1.0 获取 当前登录用户 拥有的受访页面的 按钮
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
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

        #region 2.0 判断登录用户 是否拥有 绑定 指定JS方法名 的 按钮 +bool IsBtnExist(this System.Web.Mvc.HtmlHelper htmlHelper, string strJsMethodName)
        /// <summary>
        /// 2.0 判断登录用户 是否拥有 绑定 指定JS方法名 的 按钮
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="strJsMethodName">要查找的按钮绑定的JS方法名</param>
        /// <returns></returns>
        public static bool IsButtonExist(this System.Web.Mvc.HtmlHelper htmlHelper, string strJsMethodName)
        {
            var btns = htmlHelper.ViewBag.toolbarButtons;
            var buttons = (htmlHelper.ViewBag.toolbarButtons as List<Permission>).FirstOrDefault(o => o.permissionJSMethodName.IsSame(strJsMethodName));

            return buttons != null;
        } 
        #endregion
    }
    
}
