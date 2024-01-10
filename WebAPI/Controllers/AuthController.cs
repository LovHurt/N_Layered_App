using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Entities.Concretes;
using Entities.Dtos;
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
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var loginResult = await _authService.Login(userForLoginDto);

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
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                var userExists = await _authService.UserExists(userForRegisterDto.Email);

                if (userExists != null)
                {
                    return BadRequest("User with this email already exists");
                }

                var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);

                if (registerResult == null)
                {
                    return BadRequest("Register failed");
                }

                var accessTokenResult = await _authService.CreateAccessToken(registerResult);

                if (accessTokenResult != null)
                {
                    return Ok(accessTokenResult);

                }
                return BadRequest("Access token creation failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}