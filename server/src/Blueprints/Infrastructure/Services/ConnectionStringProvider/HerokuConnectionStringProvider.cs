using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Blueprints.Infrastructure.DataAccess
{
    public class HerokuConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string PostgressTag = "DATABASE_URL";
        public string ConnectionString { get; }

        public HerokuConnectionStringProvider(IConfiguration configuration, ILogger<HerokuConnectionStringProvider> logger)
        {
            var value = configuration.GetValue<string>(PostgressTag);
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

            ConnectionString = $"Host={host};Port={port};Database={database};User Id={user};Password={password};SslMode=Require;Trust Server Certificate=true";
            logger.LogInformation(ConnectionString);
        }
    }
}