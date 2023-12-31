﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Rules;
using DataAccess.Abstract;
using Entities.Concretes;

namespace Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        IMapper _mapper;
        CategoryBusinessRules _categoryBusinessRules;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<CreatedCategoryResponse> Add(CreateCategoryRequest createCategoryRequest)
        {
            await _categoryBusinessRules.MaximumCategoryCountIsTen();

            Category category = _mapper.Map<Category>(createCategoryRequest);

            var createdCategory = await _categoryDal.AddAsync(category);

            CreatedCategoryResponse result = _mapper.Map<CreatedCategoryResponse>(createdCategory);

            return result;
        }

        public async Task<CreatedCategoryResponse> Delete(DeleteCategoryRequest deleteCategoryRequest)
        {
            Category category = await _categoryDal.GetAsync(predicate:i => i.Id == deleteCategoryRequest.Id);

            var deletedCategory = await _categoryDal.DeleteAsync(category);

            CreatedCategoryResponse result = _mapper.Map<CreatedCategoryResponse>(deletedCategory);

            return result;
        }
    }
}
