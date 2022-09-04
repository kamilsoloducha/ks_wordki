using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("repeats")]
public class RepeatsController : BaseController
{
    public RepeatsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeats([FromBody] GetRepeats.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpPost("count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeatsCount([FromBody] GetRepeatsCount.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    // [HttpPost("new/count")]
    // [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    // unused
    // public async Task<IActionResult> GetNewRepeatsCount([FromBody] GetNewRepeatsCount.Query query, CancellationToken cancellationToken)
    // => Ok(await Mediator.Send(query, cancellationToken));

}