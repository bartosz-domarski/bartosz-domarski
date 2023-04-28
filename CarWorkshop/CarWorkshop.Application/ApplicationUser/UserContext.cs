using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser? GetCurrentUser()
        {
            var User = _httpContextAccessor?.HttpContext?.User;

            if (User == null)
            {
                throw new InvalidOperationException("Context user is not present");
            }

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return null;
            }

            var id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = User.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser(id, email, roles);
        }
    }
}
