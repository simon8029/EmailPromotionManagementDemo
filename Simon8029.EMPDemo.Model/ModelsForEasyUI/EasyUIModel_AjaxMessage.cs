using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.Model.ModelsForEasyUI
{
    public class EasyUIModel_AjaxMessage
    {
        public AjaxMessageStatus Status { get; set; }
        public string Message { get; set; }
        public string  BackUrl { get; set; }
        public object Data { get; set; }
        public string rows { get; set; }

    }

    public enum AjaxMessageStatus
    {
        LoginSuccess,
        LoginFailed,
        NotLogin,
        NoPermission,
        OperationSuccess,
        OperationFailed,
        OtherError
    }
}
