using System;
using System.Net.Http;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace E2e.Tests
{
    public class E2EWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceConfig;
        public Lazy<HttpClient> HttpClient { get; }

        public E2EWebApplicationFactory(Action<IServiceCollection> serviceConfig)
        {
            _serviceConfig = serviceConfig;
            HttpClient = new Lazy<HttpClient>(CreateClient());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                _serviceConfig?.Invoke(services);
                services.AddMvc(options =>
                {
                });
            });
        }
    }
}