using System.Threading;
using System.Threading.Tasks;
using Api;
using Api.Configuration;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("repeats")]
public class RepeatsController : BaseController
{
    public RepeatsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeats([FromQuery] GetRepeats.Query query, CancellationToken cancellationToken)
        => new JsonResult(await Mediator.Send(query, cancellationToken));

    [HttpGet("count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeatsCount([FromQuery] GetRepeatsCount.Query query, CancellationToken cancellationToken)
        => new JsonResult(await Mediator.Send(query, cancellationToken));

    [HttpGet("new/count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetNewRepeatsCount([FromQuery] GetNewRepeatsCount.Query query, CancellationToken cancellationToken)
    => new JsonResult(await Mediator.Send(query, cancellationToken));

}