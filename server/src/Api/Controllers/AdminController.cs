using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        [HttpGet("env")]
        public IActionResult Env()
            => new JsonResult(configuration.AsEnumerable().Select(
                x => new { x.Key, x.Value }
            ));
    }
}