using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agathas.Storefront.Repository.EntityFramework.DataContextStorage
{
    public class ThreadDbContextStorage:IDbContextStorage
    {
        private static readonly Hashtable _DbContexts = new Hashtable();

        public DbContext GetDataContext()
        {
            DbContext DbContext = null;

            if (_DbContexts.Contains(GetThreadName()))
                DbContext = (DbContext)_DbContexts[GetThreadName()];

            return DbContext;
        }

        public void Store(DbContext DbContext)
        {
            if (_DbContexts.Contains(GetThreadName()))
                _DbContexts[GetThreadName()] = DbContext;
            else
                _DbContexts.Add(GetThreadName(), DbContext);
        }

        private static string GetThreadName()
        {
            return Thread.CurrentThread.Name;
        } 
    }
}
