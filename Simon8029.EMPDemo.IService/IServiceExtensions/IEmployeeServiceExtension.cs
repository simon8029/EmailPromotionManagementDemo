using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.IService
{
    public partial interface  IEmployeeService:IBaseService<Model.Employee>
    {
        List<Model.Permission> GetUserPermissions(int id);
    }
}


