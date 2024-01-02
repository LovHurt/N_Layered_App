using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Abstracts
{
    public interface IProductService
    {
        Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest);
        Task<CreatedProductResponse> Delete(DeleteProductRequest deleteProductRequest);

    }
}


//response request pattern
//getlistproductresponse
//automapper