using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Model.Products;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class ProductTitleRepository : Repository<ProductTitle, int>,
                                                          IProductTitleRepository
    {
        public ProductTitleRepository()
        {
        }
    }
}
