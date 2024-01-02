using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        Task<CreatedCategoryResponse> Add(CreateCategoryRequest createCategoryRequest);
        Task<CreatedCategoryResponse> Delete(DeleteCategoryRequest deleteCategoryRequest);
    }
}
