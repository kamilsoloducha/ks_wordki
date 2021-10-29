namespace Blueprints.Infrastructure.DataAccess
{
    public interface IConnectionStringProvider
    {
        string ConnectrionString { get; }
    }
}