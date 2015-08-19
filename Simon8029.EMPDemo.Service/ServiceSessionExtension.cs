using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simon8029.EMPDemo.IService;

namespace Simon8029.EMPDemo.Service
{
    public partial class ServiceSession
    {
        public int SaveChange()
        {
            return DbSessionFactory.GetDbSession().SaveChanges();
        }
    }
}
