using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace E2e.Tests
{
    public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimIdentity = new ClaimsIdentity();
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "12345678-1234-1234-1234-1234567890ab"));
        
            var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(claimIdentity), new AuthenticationProperties(),
                JwtBearerDefaults.AuthenticationScheme);
        
            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
            object resource)
        {
            return Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}