using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Blueprints.Infrastrcuture.Authentication
{
    public static class JwtAuthentication
    {
        public static IServiceCollection JwtConfig(this IServiceCollection services, IConfiguration config)
        {
            var test = config.GetSection(nameof(JwtConfiguration));
            services.Configure<JwtConfiguration>(options => config.GetSection(nameof(JwtConfiguration)).Bind(options));
            var jwtSettings = config.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

    }

}