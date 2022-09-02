using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application;
using Users.Application.Commands;

namespace Api
{
    [ApiController]
    [Route("users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator) { }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterUser.Response), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(RegisterUser.Response))]
        public async Task<IActionResult> Register([FromBody] RegisterUser.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpPut("confirm")]
        public async Task<IActionResult> Confirm(ConfirmEmail.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("login")]
        public async Task<IActionResult> Login(LoginUser.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));
        
        [HttpPut("login/chrome-extension")]
        public async Task<IActionResult> Login(LoginChromeExtension.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpDelete("delete")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteUser.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpGet("")]
        [Authorize(Policy = AuthorizationExtensions.AdminOnlyPolicy)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
            => await HandleRequest(new GetUsers.Query(), cancellationToken);


    }
}