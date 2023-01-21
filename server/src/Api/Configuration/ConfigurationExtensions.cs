using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Api.Configuration;

public static class ConfigurationExtensions {
    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
            .AddEnvironmentVariables().Build();
    }
}