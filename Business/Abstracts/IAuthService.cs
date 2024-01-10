using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        Task<User> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<User> Login(UserForLoginDto userForLoginDto);
        Task<User> UserExists(string email);
        Task<AccessToken> CreateAccessToken(User user);
    }
}
