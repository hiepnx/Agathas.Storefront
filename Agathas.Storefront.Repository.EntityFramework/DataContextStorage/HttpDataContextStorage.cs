using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace Agathas.Storefront.Repository.EntityFramework.DataContextStorage
{
    public class HttpDbContextStorage:IDbContextStorage
    {
        private string _dataContextKey = "DataContext";

        public DbContext GetDataContext()
        {
            DbContext objectContext = null;
            if (HttpContext.Current.Items.Contains(_dataContextKey))
                objectContext = (DbContext)HttpContext.Current.Items[_dataContextKey];

            return objectContext;
        }

        public void Store(DbContext DbContext)
        {
            if (HttpContext.Current.Items.Contains(_dataContextKey))
                HttpContext.Current.Items[_dataContextKey] = DbContext;
            else
                HttpContext.Current.Items.Add(_dataContextKey, DbContext);
        }
    }
}
