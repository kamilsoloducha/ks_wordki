using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api.Configuration;

public static class LoggingExtensions
{
    public static void AddCustomLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog(Log.Logger);
    }
}