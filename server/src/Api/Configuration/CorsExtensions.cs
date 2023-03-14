using Microsoft.AspNetCore.Builder;

namespace Api.Configuration
{
    public static class CorsExtensions
    {
        public static void UseCustomCors(this WebApplication app)
        {
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        }
    }
}