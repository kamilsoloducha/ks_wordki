using Blueprints.Infrastructure.DataAccess;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wordki.Tests.E2E.Feature
{
    public class TestBase
    {
        protected HttpRequestMessage Request { get; set; }
        protected HttpResponseMessage Response { get; set; }
        protected TestServerMock Host { get; set; }
        protected IConnectionStringProvider ConnectionStringProvider { get; }

        public TestBase()
        {
            Host = new TestServerMock();
        }

        [SetUp]
        protected async Task ClearDatabase()
        {
        }

        protected async Task SendRequest()
        {
            Response = await Host.Client.SendAsync(Request);
        }
    }
}
