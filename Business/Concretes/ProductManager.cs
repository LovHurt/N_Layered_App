using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Entities.Concretes;
using Core.DataAccess.Paging;
using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Concretes
{
    public class ProductManager: IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)    
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.ProductName = createProductRequest.ProductName;
            product.UnitPrice = createProductRequest.UnitPrice;
            product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
            product.UnitsInStock = createProductRequest.UnitsInStock;

             Product createdProduct = await _productDal.AddAsync(product);

             CreatedProductResponse createdProductResponse= new CreatedProductResponse();
             createdProductResponse.Id = createdProduct.Id;
             createdProductResponse.ProductName = createdProduct.ProductName;
             createdProductResponse.UnitPrice = createdProduct.UnitPrice;
             createdProductResponse.QuantityPerUnit = createdProduct.QuantityPerUnit;
             createdProductResponse.UnitsInStock = createdProduct.UnitsInStock;

             return createdProductResponse;
        }

        public async Task<List<GetListProductResponse>> GetListAsync()
        {
            // Get the product list
            var productList = await _productDal.GetListAsync();

            // Convert the product list to the GetListProductResponse type
            var mappedProductList = MapProductListToGetListProductResponseList(productList);

            return mappedProductList;
        }

        // Method that converts the product list to a list of GetListProductResponse
        private List<GetListProductResponse> MapProductListToGetListProductResponseList(IPaginate<Product> productList)
        {
            // Access the actual product list from the IPaginate object
            var products = productList.Items;

            // Perform the mapping using LINQ Select
            var mappedList = products.Select(product => new GetListProductResponse
            {
                Id = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock
            }).ToList();

            return mappedList;

        }
    }

}
