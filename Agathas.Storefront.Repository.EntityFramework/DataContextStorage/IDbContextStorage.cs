using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agathas.Storefront.Repository.EntityFramework.DataContextStorage
{
    public interface IDbContextStorage
    {
        DbContext GetDataContext();
        void Store(DbContext context);
    }
}
