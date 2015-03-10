using System;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Model.Categories;

namespace Agathas.Storefront.Model.Products
{
    public class Product : EntityBase<int>, IAggregateRoot
    {
        public int SizeId { get; set; }
        public ProductSize Size { get; set; }
        public int ProductTitleId { get; set; }
        public ProductTitle ProductTitle { get; set; }

        public string Name
        {
            get { return ProductTitle.ProductName; }
        }

        public Decimal Price
        {
            get { return ProductTitle.Price; }
        }

        public Brand Brand
        {
            get { return ProductTitle.Brand; }
        }

        public ProductColor Color
        {
            get { return ProductTitle.Color; }
        }

        public Category Category
        {
            get { return ProductTitle.Category; }
        }
        
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

}
