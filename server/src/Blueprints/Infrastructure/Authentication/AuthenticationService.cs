using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Authentication;
using Domain.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfiguration _jwtSettings;

        public AuthenticationService(IOptions<JwtConfiguration> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string Authenticate(Guid userId, IEnumerable<string> roles)
        {
            var claims = CreateClaim(userId, roles);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = SystemClock.Now.AddDays(7),
                NotBefore = SystemClock.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public string Refresh(string token)
        {
            return Authenticate(GetId(token), GetRoles(token));
        }

        private Guid GetId(string token)
        {
            var claim = GetClaimsFromToken(token).Single(x => x.Type.Equals("Id")).Value;
            return Guid.Parse(claim);
        }

        private IEnumerable<string> GetRoles(string token)
        {
            return GetClaimsFromToken(token).Where(x => x.Type.Equals("role")).Select(x => x.Value);
        }

        private IEnumerable<Claim> CreateClaim(Guid userId, IEnumerable<string> roles)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, userId.ToString());
            foreach (var role in roles)
            {
                yield return new Claim(ClaimTypes.Role, role);
            }
        }

        private IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            return securityToken.Claims;
        }
    }
}