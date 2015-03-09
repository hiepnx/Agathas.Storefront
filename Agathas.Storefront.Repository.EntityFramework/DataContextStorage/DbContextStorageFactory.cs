using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Agathas.Storefront.Repository.EntityFramework.DataContextStorage
{
    public class DbContextStorageFactory
    {
        public static IDbContextStorage _dataContectStorageContainer;

        public static IDbContextStorage CreateStorageContainer()
        {
            if (_dataContectStorageContainer == null)
            {
                if (HttpContext.Current == null)
                    _dataContectStorageContainer = new ThreadDbContextStorage();
                else
                    _dataContectStorageContainer = new HttpDbContextStorage();
            }

            return _dataContectStorageContainer;
        }
    }
}
