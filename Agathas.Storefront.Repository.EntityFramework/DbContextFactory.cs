using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Repository.EntityFramework.DataContextStorage;

namespace Agathas.Storefront.Repository.EntityFramework
{
    public class DbContextFactory
    {
        public static DbContext GetDataContext()
        {
            IDbContextStorage _dataContextStorageContainer = DbContextStorageFactory.CreateStorageContainer();

            DbContext dbContext = _dataContextStorageContainer.GetDataContext();
            if (dbContext == null)
            {
                dbContext = new ShopDbContext();
                _dataContextStorageContainer.Store(dbContext);
            }

            return dbContext;
        }       
    }
}
