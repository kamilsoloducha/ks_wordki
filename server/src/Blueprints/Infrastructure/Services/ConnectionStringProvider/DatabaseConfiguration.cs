namespace Infrastructure.Services.ConnectionStringProvider;

public class DatabaseConfiguration
{
    public string Host { get; init; }
    public int Port { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public string Database { get; init; }
}