using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application;

namespace Api
{
    [ApiController]
    [Route("users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpPut("confirm")]
        public async Task<IActionResult> Confirm(ConfirmEmail.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("login")]
        public async Task<IActionResult> Login(LoginUser.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpDelete("delete")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteUser.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpGet("")]
        [Authorize(Policy = AuthorizationExtensions.AdminOnlyPolicy)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
            => await HandleRequest(new GetUsers.Query(), cancellationToken);


    }
}