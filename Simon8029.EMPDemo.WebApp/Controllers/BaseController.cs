using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Simon8029.EMPDemo.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public OperationContext OperationContext
        {
            get
            {
                var operationContext = CallContext.GetData(typeof (OperationContext).FullName) as OperationContext;
                if (operationContext == null)
                {
                    operationContext=new OperationContext();
                    CallContext.SetData(typeof(OperationContext).FullName,operationContext);
                }

                return operationContext;
            }
        }
    }
}