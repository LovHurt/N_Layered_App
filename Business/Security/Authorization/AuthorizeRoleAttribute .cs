using Business.Abstracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection; // IServiceScopeFactory kullanabilmek için
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Security.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _requiredRole;

        public AuthorizeRoleAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Servis sağlayıcısından bir kapsam (scope) al
            using (var scope = context.HttpContext.RequestServices.CreateScope())
            {
                // Kullanıcıyı bul
                var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // userId'yi int'e çevir
                if (!int.TryParse(userId, out int userIdAsInt))
                {
                    context.Result = new ForbidResult();
                    return;
                }

                // Kullanıcıyı senkron olarak al
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = userService.GetById(userIdAsInt);

                if (user == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                // Kullanıcının rollerini asenkron olarak al
                var userRoles = await userService.GetClaims(user);

                // Gerekli rol kontrolü
                if (!userRoles.Any(role => role.Name == _requiredRole))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
