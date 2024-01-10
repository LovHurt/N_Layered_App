using Business.Messages;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security.BusinessAspects.Autofac
{

    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var httpContextAccessor = GetHttpContextAccessor();

            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var roleClaims = httpContextAccessor.HttpContext.User.ClaimRoles();

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(BusinessMessages.AuthorizationDenied);
        }
        private IHttpContextAccessor GetHttpContextAccessor()
        {
            var serviceProvider = new ServiceCollection().AddHttpContextAccessor().BuildServiceProvider();
            return serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }
    }
}
