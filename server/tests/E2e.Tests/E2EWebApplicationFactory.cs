using System;
using System.Net.Http;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace E2e.Tests;

public class E2EWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _serviceConfig;
    private string CurrentEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";
    public Lazy<HttpClient> HttpClient { get; }
    

    public E2EWebApplicationFactory(Action<IServiceCollection> serviceConfig)
    {
        _serviceConfig = serviceConfig;
        HttpClient = new Lazy<HttpClient>(CreateClient());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(CurrentEnvironment);
        builder.ConfigureLogging(loggingBuilder =>
        {
           loggingBuilder.ClearProviders();
           Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Warning()
               .WriteTo.NUnitOutput()
               .CreateLogger();
        });
        builder.ConfigureServices(services => 
        {
            _serviceConfig?.Invoke(services);
            services.AddMvc(options =>
            {
            });
            services.AddSerilog(Log.Logger);
            services.AddSingleton(_ => Log.Logger);
        });
    }
}


