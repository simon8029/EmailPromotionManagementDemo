using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.IRepository
{
    public partial  interface IDbSession
    {
        int SaveChanges();
    }
}
