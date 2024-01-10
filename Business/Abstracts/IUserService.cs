using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedUserResponse> Delete(DeleteUserRequest deleteUserRequest);
        Task<List<OperationClaim>> GetClaims(User user);
        Task<User> Add(User user);
        Task<User> GetByEmail(string email);

    }
}


