using Business;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Rules.ValidationRules;
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

    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }


        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _userService.GetListAsync(pageRequest);

            return Ok(result);
        }

        [HttpDelete]

        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest deleteUserRequest)
        {
            var result = await _userService.Delete(deleteUserRequest);

            return Ok(result);
        }

       



    }
}
