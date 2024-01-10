using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var loginResult = await _authService.Login(user);

            if (loginResult == null) 
            {
                return BadRequest("Login failed");
            }

            var accessTokenResult = await _authService.CreateAccessToken(loginResult);

            if (accessTokenResult == null) 
            {
                return BadRequest("Access token creation failed");
            }

            return Ok(accessTokenResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest createUserRequest)
        {
            User user = _mapper.Map<User>(createUserRequest);

            var userExists = await _authService.UserExists(user);

            if (userExists != null)
            {
                return BadRequest("Register failed");
            }

            var registerResult = await _authService.Register(user);

            var accessTokenResult = await _authService.CreateAccessToken(registerResult);

            if (accessTokenResult != null)
            {
                return Ok(accessTokenResult);

            }
            return BadRequest("Access token creation failed");

        }
    }
}
