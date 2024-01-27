using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands;
using Users.Application.Queries;

namespace Api.Features.Users;

[ApiController]
[Route("users")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUser.Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RegisterUser.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] Api.Model.Requests.RegisterUser request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUser.Command(request.UserName, request.Password, request.Email, string.Empty,
            string.Empty);

        var result = await Mediator.Send(command, cancellationToken);

        return result.ResponseCode == RegisterUser.ResponseCode.Successful ? Ok(result) : BadRequest(result);
    }

    [HttpPut("confirm")]
    public async Task<IActionResult> Confirm(ConfirmEmail.Command command, CancellationToken cancellationToken)
        => await HandleRequest(command, cancellationToken);

    [HttpPut("login")]
    [ProducesResponseType(typeof(LoginUser.Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(LoginUser.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] Api.Model.Requests.LoginUser request,
        CancellationToken cancellationToken)
    {
        var command = new LoginUser.Command(request.UserName, request.Password);
                
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("login/chrome-extension")]
    public async Task<IActionResult> Login(LoginChromeExtension.Command command,
        CancellationToken cancellationToken)
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