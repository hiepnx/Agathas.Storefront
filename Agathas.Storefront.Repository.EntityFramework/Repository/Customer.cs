using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Model.Customers;

namespace Agathas.Storefront.Repository.EntityFramework.Repository
{
    public class CustomerRepository : Repository<Customer, int>,
                                          ICustomerRepository
    {
        public CustomerRepository()
        {
        }

        public Customer FindBy(string identityToken)
        {
            Customer customer = this.FindOne(i => i.AuthenticationToken == identityToken);
            return customer;
        }
    }
}
