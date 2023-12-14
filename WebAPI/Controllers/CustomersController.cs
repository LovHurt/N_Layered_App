using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("addcustomer")]
        public async Task<IActionResult> Add([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var result = await _customerService.Add(createCustomerRequest);

            return Ok(result);
        }

        [HttpGet("getlistasynccustomer")]
        public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
        {
            var result = _customerService.GetListAsync(pageRequest);

            return Ok(result);
        }

        [HttpGet("getbyidasynccustomer")]
        public async Task<IActionResult> GetByIdAsync([FromBody] string id)
        {
            var result = _customerService.GetById(id);

            return Ok(result);
        }

        [HttpDelete("deletecustomer")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCustomerRequest deleteCustomerRequest)
        {
            var result = _customerService.Delete(deleteCustomerRequest);
            return Ok(result);
        }

        [HttpPut("updatecustomer")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            var result = _customerService.Update(updateCustomerRequest);

            return Ok(result);
        }
    }
}
