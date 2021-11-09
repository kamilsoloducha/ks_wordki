namespace Blueprints.Infrastructure.DataAccess
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }
    }
}