﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Infrastructure.Domain;

namespace Agathas.Storefront.Model.Customers
{
    public interface ICustomerRepository : INHRepository<Customer, int>
    {
        Customer FindBy(string identityToken);
    }

}
