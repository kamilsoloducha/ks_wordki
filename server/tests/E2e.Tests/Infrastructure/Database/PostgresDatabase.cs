using System.Threading.Tasks;
using Infrastructure.Services.ConnectionStringProvider;
using Testcontainers.PostgreSql;

namespace E2e.Tests.Infrastructure.Database;

public class PostgresDatabase
{
    private const string Image = "postgres:alpine";
    public const string Username = "root";
    public const string Password = "password";
    public const string Database = "Wordki";
    public const int Port = 5432;

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
            .WithPortBinding(Port, Port)
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

public sealed class ContainerConnectionProvider : IConnectionStringProvider
{
    public string ConnectionString =>
        $"Host=localhost;Port={PostgresDatabase.Port};Database={PostgresDatabase.Database};User Id={PostgresDatabase.Username};Password={PostgresDatabase.Password};";
}