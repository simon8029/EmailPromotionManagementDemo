using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.IRepository;

namespace Simon8029.EMPDemo.Service
{
    class DbSessionFactory
    {
        //[ThreadStatic]
        //private static IRespository.IDBSession _idbSession = null;

        public static IRepository.IDbSession GetDbSession()
        {
            //if (_idbSession == null) _idbSession = Utility.DI.GetObject<IRespository.IDBSession>("dalSession");
            //return _idbSession;


            //1.从线程中取出 键 对应的值
            var db = CallContext.GetData("IDbSession") as IDbSession;
            //2.如果为空（线程中不存在）
            if (db == null)
            {
                //3.实例化 EF容器 子类对象
                db = Utilities.DI.GetObject<IRepository.IDbSession>("DbSession");
                //4.并存入线程
                CallContext.SetData("IDbSession", db);
            }
            //5.返回DBSession对象
            return db;
        }
    }
}
