using Microsoft.Extensions.Options;

namespace Blueprints.Infrastructure.DataAccess
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public ConnectionStringProvider(IOptions<DatabaseConfiguration> options)
        {
            var config = options.Value;
            ConnectionString = $"Host={config.Host};Port={config.Port};Database={config.Database};User Id={config.User};Password={config.Password};";
            ConnectionString = "Host=ec2-46-137-156-205.eu-west-1.compute.amazonaws.com;Port=5432;Database=d3eoufsd95lnc0;User Id=bqobwxfagmhqby;Password=6b444f3e788275e0c8ac2809756616f7597c70abc06c1e5d57d9870f1cf84723;SslMode=Require;Trust Server Certificate=true";
        }
        public string ConnectionString { get; }
    }
}