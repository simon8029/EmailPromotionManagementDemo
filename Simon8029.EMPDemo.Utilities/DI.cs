using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;

namespace Simon8029.EMPDemo.Utilities
{
    public class DI
    {
        [ThreadStatic]
        private static IApplicationContext objFactory = null;

        private static IApplicationContext GetFactory()
        {
            if (objFactory == null) objFactory = Spring.Context.Support.ContextRegistry.GetContext();
            return objFactory;
        }

        public static T GetObject<T>(string objectName) where T : class
        {
            return (T)GetFactory().GetObject(objectName);
        }
    }
}
