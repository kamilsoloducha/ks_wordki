using System;
using System.Linq;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly IConnectionStringProvider connectionStringProvider;

        public AdminController(IConfiguration configuration,
            IServiceProvider serviceProvider,
            IConnectionStringProvider connectionStringProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            this.connectionStringProvider = connectionStringProvider;
        }


        [HttpGet("env")]
        public IActionResult Env()
            => Ok(configuration.AsEnumerable().Select(
                x => new { x.Key, x.Value }
            ));

        [HttpGet("connectionString")]
        public IActionResult ConnectionString()
            => Ok(connectionStringProvider.ConnectionString);
    }
}