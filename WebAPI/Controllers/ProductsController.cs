using Business;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Rules.ValidationRules;
using Business.Security.BusinessAspects.Autofac;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Logging;
using Core.DataAccess.Paging;
using Entities.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [SecuredOperation("product.add,admin")]
        [ValidateModel(typeof(CreateProductRequestValidator))]
        [LogActionFilter]
        public async Task<IActionResult> Add([FromBody] CreateProductRequest createProductRequest)
        {

            var result = await _productService.Add(createProductRequest); 

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _productService.GetListAsync(pageRequest); 

            return Ok(result);
        }
        [HttpDelete]
        [LogActionFilter]

        public async Task<IActionResult> Delete([FromBody] DeleteProductRequest deleteProductRequest)
        {
            var result = await _productService.Delete(deleteProductRequest);

            return Ok(result);
        }
    }
}
