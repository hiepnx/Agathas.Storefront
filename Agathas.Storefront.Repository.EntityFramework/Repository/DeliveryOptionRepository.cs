using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Model.Shipping;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class DeliveryOptionRepository : Repository<DeliveryOption, int>,
                                                           IDeliveryOptionRepository
    {
        public DeliveryOptionRepository()
        {
        }
    }
}
