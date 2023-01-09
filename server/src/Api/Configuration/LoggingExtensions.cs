using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Api.Configuration;

public static class LoggingExtensions
{
    public static void AddCustomLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog(logger);
    }
}
