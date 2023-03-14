using System;
using System.Linq;
using System.Security.Claims;
using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDataProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Id");
            return userIdClaim is null ? Guid.Empty : Guid.Parse(userIdClaim.Value);
        }

        public bool IsAdmin()
        {
            return _httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .Any(x => x == "Admin");
        }
    }
}