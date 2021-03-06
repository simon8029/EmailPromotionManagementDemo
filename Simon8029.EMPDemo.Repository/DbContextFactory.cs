﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.Repository
{
    public static class DbContextFactory
    {
        //[ThreadStatic]
        //public static DbContext dbContext = new Entities();

        public static DbContext GetDbContext()
        {
            #region 从线程中操作数据的普通方式
            //1. 从线程中取出键对应的EF容器子类对象
            var dbContext = CallContext.GetData("DbContext") as DbContext;
            //2.如果为空（线程中不存在该对象），则实例化EF容器子类对象，并将其存入线程中
            if (dbContext == null)
            {
                dbContext = new Entities();
                CallContext.SetData("DbContext", dbContext);
                }
            //3.返回EF容器子类对象
            return dbContext; 
            #endregion
        }

    }
}
