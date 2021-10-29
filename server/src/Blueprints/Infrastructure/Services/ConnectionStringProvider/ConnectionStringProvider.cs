using Microsoft.Extensions.Options;

namespace Blueprints.Infrastructure.DataAccess
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {

        public ConnectionStringProvider(IOptions<DatabaseConfiguration> options)
        {
            var config = options.Value;
            ConnectrionString = $"Host={config.Host};Port={config.Port};Database={config.Database};User Id={config.User};Password={config.Password};";
        }

        public string ConnectrionString { get; }

    }
}