using Business;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Rules.ValidationRules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.DataAccess.Paging;
using Entities.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        IValidator<CreateProductRequest> _validator;

        public ProductsController(IProductService productService, IValidator<CreateProductRequest> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpPost]

        public async Task<IActionResult> Add([FromBody] CreateProductRequest createProductRequest)
        {
            _validator.ValidateAndThrow(createProductRequest);
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
        public async Task<IActionResult> Delete([FromBody] DeleteProductRequest deleteProductRequest)
        {
            var result = await _productService.Delete(deleteProductRequest);

            return Ok(result);
        }
    }
}
