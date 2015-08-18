using System.Runtime.Remoting.Messaging;

namespace Simon8029.EMPDemo.WebApp
{
    public class OperationContext
    {
        public const string UserSessionKey = "USER_SESSION_KEY";
        public const string UserPerSessionKey = "USER_PER_SESSION_KEY";
        public const string UseridCookieKey = "USERID_COOKIE_KEY";

        public static OperationContext Current
        {
            get
            {
                var operationContext = CallContext.GetData(typeof (OperationContext).FullName) as OperationContext;
                if (operationContext == null)
                {
                    operationContext = new OperationContext();
                    CallContext.SetData(typeof (OperationContext).FullName, operationContext);
                }

                return operationContext;
            }
        }
       
        IService.IServiceSession _bllSession;
        #region 1.0 业务仓储 +IServiceSession BLLSession
        /// <summary>
        /// 1.0 业务仓储 
        /// </summary>
        public IService.IServiceSession BLLSession
        {
            get
            {
                //1.如果为空
                if (_bllSession == null)
                {
                    //2.实例化 业务仓储 对象
                    _bllSession = Utility.DI.GetObject<IService.IServiceSession>("bllSession");
                }
                //3.返回业务仓储对象
                return _bllSession;
            }
        }
    }
}