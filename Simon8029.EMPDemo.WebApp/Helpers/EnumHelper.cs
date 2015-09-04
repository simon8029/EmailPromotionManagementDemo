using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simon8029.EMPDemo.WebApp.Helpers
{
    /// <summary>
    /// 通过EnumHelper管理自定义枚举数据
    /// 自定义枚举数据 ： 主要用来保存 数据库中没有保存 但 实际开发中使用的 枚举数据
    /// 如 权限表的请求方式：1-GET 2-POST 3-BOTH
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        ///权限的 请求方式
        /// </summary>
        public static class FormMethod
        {
            public static int GET = 1;
            public static int POST = 2;
            public static int BOTH = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
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

        /// <summary>
        /// 权限的 类型
        /// </summary>
        public static class OperationType
        {
            public static int MENU = 1;
            public static int BUTTON = 2;
            public static int AJAX = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
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

        /// <summary>
        /// 权限图标 类选择器名称
        /// </summary>
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
            /// <summary>
            /// 下拉框数据
            /// </summary>
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


        /// <summary>
        /// 申请单优先级
        /// </summary>
        public static class ApplyPriority
        {
            public static int HIGE = 1;
            public static int NORMAL = 2;
            public static int LOW = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
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

        /// <summary>
        /// 申请单状态
        /// </summary>
        public static class ApplyStatue
        {
            /// <summary>
            /// 运行中
            /// </summary>
            public const short RUNING = 1;
            /// <summary>
            /// 已通过
            /// </summary>
            public const short PASSED = 2;
            /// <summary>
            /// 已拒绝
            /// </summary>
            public const short REFUSED = 3;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="运行中" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="已通过" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="已拒绝" }
                    };
                    return _ddlData;
                }
            }
        }

        /// <summary>
        /// 申请单 审批操作 类型
        /// </summary>
        public static class ApplyOperation
        {
            /// <summary>
            /// 提交 -1
            /// </summary>
            public const int Submit = 1;
            /// <summary>
            /// 通过 -2
            /// </summary>
            public const int Pass = 2;
            /// <summary>
            /// 驳回-3
            /// </summary>
            public const int Reject = 3;
            /// <summary>
            /// 拒绝 -4
            /// </summary>
            public const int Refuse = 4;

            static List<System.Web.Mvc.SelectListItem> _ddlData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
            public static List<System.Web.Mvc.SelectListItem> DDLData
            {
                get
                {
                    if (_ddlData == null)
                        _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="1",Text="提交" },
                       new System.Web.Mvc.SelectListItem(){Value="2",Text="通过" },
                       new System.Web.Mvc.SelectListItem(){Value="3",Text="驳回" },
                       new System.Web.Mvc.SelectListItem(){Value="4",Text="拒绝" }
                    };
                    return _ddlData;
                }
            }
        }

        
    }
}
