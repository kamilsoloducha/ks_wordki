using System.Threading.Tasks;
using E2e.Tests.Infrastructure.Database;
using NUnit.Framework;

namespace Cards.E2e.Tests;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public async Task SetupSet()
    {
        await PostgresDatabase.Instance.StartContainer();
    }

    [OneTimeTearDown]
    public async Task TearDownSet()
    {
        await PostgresDatabase.Instance.StopContainer();
    }
}
