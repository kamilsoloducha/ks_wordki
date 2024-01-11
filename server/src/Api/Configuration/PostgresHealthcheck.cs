using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Api.Configuration;

internal class PostgresHealthcheck(IConnectionStringProvider connectionStringProvider) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var connectionString = connectionStringProvider.ConnectionString;
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            var connection = await dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = dataSource.CreateCommand("Select 1");
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            await reader.ReadAsync(cancellationToken);
            await connection.CloseAsync();
            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(e.Message);
        }
    }
}