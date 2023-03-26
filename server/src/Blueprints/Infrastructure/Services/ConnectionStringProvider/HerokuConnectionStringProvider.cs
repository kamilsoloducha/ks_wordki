using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.ConnectionStringProvider;

public class HerokuConnectionStringProvider : IConnectionStringProvider
{
    private const string PostgressTag = "DATABASE_URL";
    public string ConnectionString { get; }

    public HerokuConnectionStringProvider(IConfiguration configuration, ILogger<HerokuConnectionStringProvider> logger)
    {
        ConnectionString = CreateConnectionString(configuration, logger);
    }

    private static string CreateConnectionString(IConfiguration configuration, ILogger<HerokuConnectionStringProvider> logger)
    {
        var value = configuration.GetValue<string>(PostgressTag);

        if (string.IsNullOrEmpty(value))
        {
            logger.LogWarning($"There is no {PostgressTag} value in settings. Connection to db may be impossible.");
            return string.Empty;
        }
        logger.LogInformation(value);
        value = value.Remove(0, "postgres://".Length);
        var counter = value.IndexOf(':');
        var user = value.Substring(0, counter);
        value = value.Remove(0, counter + 1);

        counter = value.IndexOf('@');
        var password = value.Substring(0, counter);
        value = value.Remove(0, counter + 1);

        counter = value.IndexOf(':');
        var host = value.Substring(0, counter);
        value = value.Remove(0, counter + 1);

        counter = value.IndexOf('/');
        var port = value.Substring(0, counter);
        value = value.Remove(0, counter + 1);

        var database = value;

        return $"Host={host};Port={port};Database={database};User Id={user};Password={password};SslMode=Require;Trust Server Certificate=true";
    }

    public static bool IsHerokuEnv(IConfiguration configuration)
    {
        return !string.IsNullOrEmpty(configuration.GetValue<string>(PostgressTag));
    }
}