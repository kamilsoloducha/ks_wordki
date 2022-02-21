using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Wordki.Tests.Utils.ServerMock
{
    public abstract class ServerMock<TStartup> where TStartup : class
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public ServerMock()
        {
            CreateMocks();
            CreateServer();
        }

        private void CreateServer()
        {
            Server = new TestServer(new WebHostBuilder()
                .ConfigureTestServices(ConfigureTestContainer)
                .ConfigureServices(ConfigureServices)
                .UseEnvironment("Test")
                .UseStartup<TStartup>());
            Client = Server.CreateClient();
        }

        protected abstract void CreateMocks();
        protected abstract void ConfigureTestContainer(IServiceCollection services);
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            });
        }
    }
}
