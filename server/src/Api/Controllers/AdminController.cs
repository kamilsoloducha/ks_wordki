using System;
using System.Linq;
using System.Threading.Tasks;
using Blueprints.Infrastructure.DataAccess;
using Cards.Infrastructure;
using Lessons.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Users.Infrastructure;

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
            => new JsonResult(configuration.AsEnumerable().Select(
                x => new { x.Key, x.Value }
            ));

        [HttpGet("dbInit")]
        public async Task<IActionResult> Init()
        {
            await serviceProvider.CreateUsersDb();
            await serviceProvider.CreateCardsDb();
            await serviceProvider.CreateLessonsDb();
            return Ok();
        }

        [HttpGet("connectionString")]
        public IActionResult ConnectionString()
            => new JsonResult(connectionStringProvider.ConnectionString);
    }
}