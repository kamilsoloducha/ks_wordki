using System;
using System.Linq;
using System.Threading.Tasks;
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

        public AdminController(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
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
    }
}