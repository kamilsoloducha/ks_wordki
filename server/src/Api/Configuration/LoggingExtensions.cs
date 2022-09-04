using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Configuration;

public static class LoggingExtensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        return services.AddLogging(loggingBuilder =>
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            loggingBuilder.AddSeq(configuration.GetSection("Seq"));
        });
    }
}