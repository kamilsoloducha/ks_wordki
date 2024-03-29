using Microsoft.Extensions.Options;

namespace Infrastructure.Services.ConnectionStringProvider;

public class ConnectionStringProvider : IConnectionStringProvider
{
    public ConnectionStringProvider(IOptions<DatabaseConfiguration> options)
    {
        var config = options.Value;
        ConnectionString = $"Host={config.Host};Port={config.Port};Database={config.Database};User Id={config.User};Password={config.Password};";
    }
    public string ConnectionString { get; }
}