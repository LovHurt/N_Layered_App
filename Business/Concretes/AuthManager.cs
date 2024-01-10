using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Messages;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;
public class AuthManager : IAuthService
{
    private IUserService _userService;
    private ITokenHelper _tokenHelper;
    IMapper _mapper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {
        var claims = await _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);

        return accessToken;
    }

    public async Task<User> Login(User user)
    {
        var userToCheck = await _userService.GetByEmail(user);

        if (userToCheck == null)
        {
            throw new BusinessException(BusinessMessages.UserCanNotBeFound);
        }

        if (!HashingHelper.VerifyPasswordHash(user.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            throw new BusinessException(BusinessMessages.PasswordError);
        }

        return userToCheck;
    }

    public async Task<User> Register(User userx)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userx.Password, out passwordHash, out passwordSalt);

        var user = new User
        {
            Email = userx.Email,
            FirstName = userx.FirstName,
            LastName = userx.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };

        var addedUser = await _userService.Add(user);

        User result = _mapper.Map<User>(addedUser);

        return result;
    }

    public async Task<User> UserExists(User user)
    {
        if (_userService.GetByEmail(user) != null)
        {
            throw new BusinessException(BusinessMessages.UserAlreadyExists);

        }
        return user;
    }
}

