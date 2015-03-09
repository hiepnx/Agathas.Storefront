using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Model.Categories;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository()            
        {
        }
    }
}
