using System;
using System.Collections.Generic;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Model.Categories;

namespace Agathas.Storefront.Model.Products
{
    public class ProductTitle : IAggregateRoot
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ColorId { get; set; }
        public ProductColor Color { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

}
