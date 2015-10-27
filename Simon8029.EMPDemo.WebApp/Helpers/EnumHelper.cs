using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simon8029.EMPDemo.WebApp.Helpers
{
   
    public static class EnumHelper
    {
        public static class FormMethod
        {
            public static int GET = 1;
            public static int POST = 2;
            public static int BOTH = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
           
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="GET" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="POST" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="BOTH" }
                    };
                    return _ddlData;
                }
            }
        }

        
        public static class OperationType
        {
            public static int MENU = 1;
            public static int BUTTON = 2;
            public static int AJAX = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="MENU" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="BUTTON" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="AJAX" }
                    };
                    return _ddlData;
                }
            }
        }

        
        public static class IconClassName
        {
            public static string IconAdd = "icon-add";
            public static string IconEdit = "icon-edit";
            public static string IconRemove = "icon-remove";
            public static string IconCut = "icon-cut";
            public static string IconSave = "icon-save";
            public static string IconOk = "icon-ok";
            public static string IconNo = "icon-no";
            public static string IconCancel = "icon-cancel";
            public static string IconSearch = "icon-search";
            public static string IconTip = "icon-tip";

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="icon-add",Text="icon-add" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-edit",Text="icon-edit" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-remove",Text="icon-remove" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-cut",Text="icon-cut" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-save",Text="icon-save" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-ok",Text="icon-ok" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-no",Text="icon-no" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-cancel",Text="icon-cancel" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-search",Text="icon-search" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-tip",Text="icon-tip" }
                    };
                    return _ddlData;
                }
            }
        }


        
        public static class ApplyPriority
        {
            public static int HIGE = 1;
            public static int NORMAL = 2;
            public static int LOW = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="HIGH" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="NORMAL" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="LOW" }
                    };
                    return _ddlData;
                }
            }
        }

       
        public static class ApplyStatue
        {
            
            public const short RUNING = 1;
           
            public const short PASSED = 2;
           
            public const short REFUSED = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
           
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="running" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="pass" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="decline" }
                    };
                    return _ddlData;
                }
            }
        }

       
        public static class ApplyOperation
        {
          
            public const int Submit = 1;
            
            public const int Pass = 2;
            
            public const int Reject = 3;
          
            public const int Refuse = 4;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="submit" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="approved" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="reject" },
                       new System.Web.Mvc.SelectListItem(){Value="4",Text="decline" }
                    };
                    return _ddlData;
                }
            }
        }

        
    }
}
