using System.Threading.Tasks;
using NUnit.Framework;

namespace Users.E2e.Tests;

[SetUpFixture]
[NonParallelizable]
public class TestSetup
{
    [OneTimeSetUp]
    public async Task SetupSet()
    {
        //await PostgresDatabase.Instance.StartContainer();
    }

    [OneTimeTearDown]
    public async Task TearDownSet()
    {
        //await PostgresDatabase.Instance.StopContainer();
    }
}
