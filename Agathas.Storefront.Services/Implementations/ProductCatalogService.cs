using System.Collections.Generic;
using System.Linq;
using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Products;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;
using Agathas.Storefront.Repository.EntityFramework;
using System;
using System.Linq.Expressions;
using Agathas.Storefront.Infrastructure.Specification;

namespace Agathas.Storefront.Services.Implementations
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductTitleRepository _productTitleRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductCatalogService(IProductTitleRepository productTitleRepository,
                                       IProductRepository productRepository,
                                       ICategoryRepository categoryRepository)
        {
            _productTitleRepository = productTitleRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        private IEnumerable<Product> GetAllProductsMatchingQueryAndSort(GetProductsByCategoryRequest request)
        {
            IEnumerable<Product> productsMatchingRefinement = _productRepository.Find(new Agathas.Storefront.Infrastructure.Specification.Specification<Product>(p=>p.ProductTitle.CategoryId == request.CategoryId));
           // new Agathas.Storefront.Infrastructure.Specification.Specification<Product>(p=>p.ProductTitle.CategoryId == request.CategoryId));

            switch (request.SortBy)
            {
                case ProductsSortBy.PriceLowToHigh:
                    productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Price);
                    break;
                case ProductsSortBy.PriceHighToLow:
                    productsMatchingRefinement = productsMatchingRefinement.OrderByDescending(p => p.Price);
                    break;
            }
            return productsMatchingRefinement;
        }


        public GetFeaturedProductsResponse GetFeaturedProducts()
        {
            GetFeaturedProductsResponse response = new GetFeaturedProductsResponse();
            //response.Products = _productTitleRepository.GetQuery(p=>1==1,0,6).ConvertToProductViews();
            response.Products = _productTitleRepository.Get<int>(x => x.Id, 1, 6).ConvertToProductViews();
            return response;
        }

        public GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request)
        {
            GetProductsByCategoryResponse response;
            Specification<Product> spec = new Specification<Product>(p=>p.ProductTitle.CategoryId == request.CategoryId);
            if ((request.ColorIds != null) && (request.ColorIds.Count() > 0))
            {
              spec=  spec.And(new Specification<Product>(q=>request.ColorIds.Contains(q.ProductTitle.ColorId)));
            }
            IEnumerable<Product> productsMatchingRefinement = _productRepository.Find(spec);
            var args = new List<Expression<Func<Product, object>>>();
            
                args.Add(u => u.ProductTitle.Brand);
                args.Add(v => v.ProductTitle.Color);
                args.Add(w => w.Size);
                productsMatchingRefinement = (productsMatchingRefinement as IQueryable<Product>).GetAllIncluding(args);
            response = productsMatchingRefinement.CreateProductSearchResultFrom(request);
            return response;
        }

        public GetProductResponse GetProduct(GetProductRequest request)
        {
            GetProductResponse response = new GetProductResponse();

            ProductTitle productTitle = _productTitleRepository.FindBy(request.ProductId);

            response.Product = productTitle.ConvertToProductDetailView();

            return response;
        }

        public GetAllCategoriesResponse GetAllCategories()
        {
            GetAllCategoriesResponse response = new GetAllCategoriesResponse();
            IEnumerable<Category> categories = _categoryRepository.FindAll();
            response.Categories = categories.ConvertToCategoryViews();

            return response;
        }
    }
}
