using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using Testcontainers.PostgreSql;

namespace E2e.Tests.Infrastructure.Database;

public class PostgresDatabase
{
    private const string Image = "postgres:alpine";
    public const string Username = "root";
    public const string Password = "password";
    public const string Database = "Wordki";
    private const int InternalPostgresPort = 5432;
    public const int Port = 5433;

    private static PostgresDatabase _instance;

    public static PostgresDatabase Instance => _instance ??= new PostgresDatabase();

    private readonly PostgreSqlContainer _container;

    private PostgresDatabase()
    {
        _container = new PostgreSqlBuilder()
            .WithImage(Image)
            .WithDatabase(Database)
            .WithUsername(Username)
            .WithPassword(Password)
            .WithPortBinding(Port, InternalPostgresPort)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(InternalPostgresPort))
            .Build();
    }

    public Task StartContainer()
    {
        return _container.StartAsync();
    }

    public Task StopContainer()
    {
        return _container.StopAsync();
    }
}