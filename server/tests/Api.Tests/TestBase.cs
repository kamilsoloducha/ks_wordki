using Blueprints.Infrastructure.DataAccess;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Tests;
using Utils;

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
            SystemClock.Override(TestServerMock.MockDate);
        }

        protected async Task SendRequest()
        {
            Response = await Host.Client.SendAsync(Request);
        }
    }
}
