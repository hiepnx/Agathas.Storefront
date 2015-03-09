using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Model.Basket;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class BasketRepository : Repository<Basket, Guid>, IBasketRepository
    {
        public BasketRepository()            
        {
        }
    }
}
