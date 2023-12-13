using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Entities.Concretes;
using Core.DataAccess.Paging;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Rules;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes
{
    public class ProductManager: IProductService
    {
        IProductDal _productDal;
        IMapper _mapper;
        ProductBusinessRules _productBusinessRules;

        public ProductManager(ProductBusinessRules productBusinessRules, IMapper mapper, IProductDal productDal)
        {
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
            _productDal = productDal;
        }


        public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
        {
            await _productBusinessRules.MaxProductCountPerCategoryTwenty(createProductRequest.CategoryId);

            Product mappedRequest = _mapper.Map<Product>(createProductRequest);

            Product createdProduct = await _productDal.AddAsync(mappedRequest);

            CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(createdProduct);

            return createdProductResponse;
        }

        public async Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _productDal.GetListAsync(
                include: p => p.Include(p => p.Category),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            var result = _mapper.Map<Paginate<GetListProductResponse>>(data);
            return result;

            //var productList = await _productDal.GetListAsync();
            //var mappedList = _mapper.Map<Paginate<GetListProductResponse>>(productList);
            //return mappedList;
        }

    }

}
