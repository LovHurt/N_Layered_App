using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
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
        Task<User> Register(User user);
        Task<User> Login(User user);
        Task<User> UserExists(User user);
        Task<AccessToken> CreateAccessToken(User user);
    }
}
