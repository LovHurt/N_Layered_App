using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Messages;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using Entities.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = await _userService.GetByEmail(userForLoginDto.Email);

        if (userToCheck == null)
        {
            throw new BusinessException(BusinessMessages.UserCanNotBeFound);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            throw new BusinessException(BusinessMessages.PasswordError);
        }

        return userToCheck;
    }

    public async Task<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        var user = new User
        {
            Email = userForRegisterDto.Email,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };

        var result = await _userService.Add(user);

        return result;
    }

    public async Task<User> UserExists(string email)
    {

        var existingUser = await _userService.GetByEmail(email);

        if (existingUser != null)
        {
            //throw new InvalidOperationException("User already exists");
            return existingUser;
        }

        return null;
    }
}

