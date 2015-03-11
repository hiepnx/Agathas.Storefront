using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agathas.Storefront.Model.Basket;
using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Customers;
using Agathas.Storefront.Model.Orders;
using Agathas.Storefront.Model.Products;
using Agathas.Storefront.Model.Shipping;

namespace Agathas.Storefront.Repository.EntityFramework
{
    public class ShopDbContext : DbContext
    {

        public DbSet<Basket> _baskets;
        public DbSet<BasketItem> _basketItems;
        public DbSet<Brand> _brands;
        public DbSet<Category> _categories;
        public DbSet<Customer> _customers;
        public DbSet<DeliveryAddress> _deliveryAddresses;
        public DbSet<Order> _orders;
        public DbSet<OrderItem> _orderItems;
        public DbSet<Product> _products;
        public DbSet<Color> _colors;
        public DbSet<Size> _sizes;
        public DbSet<ProductTitle> _productTitles;
        public DbSet<Courier> _couriers;
        public DbSet<ShippingService> _courierServices;

        public ShopDbContext()
            : base("ShopDB")
        {
            //_baskets = CreateDbSet<Basket>();
            //_basketItems = CreateDbSet<BasketItem>();
            //_brands = CreateDbSet<Brand>();
            //_categories = CreateDbSet<Category>();
            //_customers = CreateDbSet<Customer>();
            //_deliveryAddresses = CreateDbSet<DeliveryAddress>();
            //_orderItems = CreateDbSet<OrderItem>();
            //_orders = CreateDbSet<Order>();
            //_products = CreateDbSet<Product>();
            //_colors = CreateDbSet<ProductColor>();
            //_sizes = CreateDbSet<ProductSize>();
            //_productTitles = CreateDbSet<ProductTitle>();
            //_couriers = CreateDbSet<Courier>();
            //_courierServices = CreateDbSet<ShippingService>();
            //base.ContextOptions.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            
            
        }

        public DbSet<Basket> Baskets
        {
            get
            {
                return _baskets;
            }
        }
        public DbSet<BasketItem> BasketItems
        {
            get
            {
                return _basketItems;
            }
        }
        public DbSet<Brand> Brands
        {
            get
            {
                return _brands;
            }
        }
        public DbSet<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
        public DbSet<Customer> Customers
        {
            get
            {
                return _customers;
            }
        }
        public DbSet<DeliveryAddress> DeliveryAddresses
        {
            get
            {
                return _deliveryAddresses;
            }
        }
        public DbSet<Order> Orders
        {
            get
            {
                return _orders;
            }
        }
        public DbSet<OrderItem> OrderItems
        {
            get
            {
                return _orderItems;
            }
        }
        public DbSet<Product> Products
        {
            get
            {
                return _products;
            }
        }
        public DbSet<Color> Colors
        {
            get
            {
                return _colors;
            }
        }
        public DbSet<Size> Sizes
        {
            get
            {
                return _sizes;
            }
        }
        public DbSet<ProductTitle> ProductTitles
        {
            get
            {
                return _productTitles;
            }
        }
        public DbSet<Courier> Couriers
        {
            get
            {
                return _couriers;
            }
        }
        public DbSet<ShippingService> CourierServices
        {
            get
            {
                return _courierServices;
            }
        }
    }
}
