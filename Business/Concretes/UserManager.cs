﻿using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Entities.Concretes;
using Core.DataAccess.Paging;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Rules;
using Microsoft.EntityFrameworkCore;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;
using Business.Rules.ValidationRules;
using System.Security.Policy;
using System.Security.Cryptography;
using DataAccess.Concretes;
using DataAccess.Contexts;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IMapper _mapper;
        UserBusinessRules _UserBusinessRules;

        public UserManager(UserBusinessRules UserBusinessRules, IMapper mapper, IUserDal UserDal)
        {
            _UserBusinessRules = UserBusinessRules;
            _mapper = mapper;
            _userDal = UserDal;
        }


        public async Task<CreatedUserResponse> Delete(DeleteUserRequest deleteUserRequest)
        {
            User User = await _userDal.GetAsync(predicate: p => p.Id == deleteUserRequest.Id);

            var deletedUser = await _userDal.DeleteAsync(User);

            var result = _mapper.Map<CreatedUserResponse>(deletedUser);

            return result;
        }

        public async Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _userDal.GetListAsync(
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            var result = _mapper.Map<Paginate<GetListUserResponse>>(data);
            return result;

        }


        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var data = await _userDal.GetListAsync(
                include: q => q.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim)
            );

            var userClaims = data.Items
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.UserOperationClaims)
                .Select(uoc => uoc.OperationClaim)
                .ToList();

            return userClaims;
        }

        public async Task<CreatedUserResponse> Add(User user)
        {

            var createdUser = await _userDal.AddAsync(user);

            CreatedUserResponse result = _mapper.Map<CreatedUserResponse>(createdUser);

            return result;
        }

        public async Task<User> GetByEmail(User user)
        {
            var result = await _userDal.GetAsync(c => c.Email == user.Email);

            return result;
        }
    }

}