namespace Infrastructure.Services.ConnectionStringProvider;

public interface IConnectionStringProvider
{
    string ConnectionString { get; }
}