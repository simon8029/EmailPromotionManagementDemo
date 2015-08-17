using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.Repository
{
    public partial class DbSession:IRepository.IDbSession
    {
        public int SaveChanges()
        {
            return DbContextFactory.GetDbContext().SaveChanges();
        }
    }
}
