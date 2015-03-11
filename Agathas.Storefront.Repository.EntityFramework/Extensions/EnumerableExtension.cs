using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Agathas.Storefront.Repository.EntityFramework
{
    public static class EnumerableExtension
    {
        public static IQueryable<T> GetAllIncluding<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}
