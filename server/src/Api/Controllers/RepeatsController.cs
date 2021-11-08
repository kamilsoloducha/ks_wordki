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

    [HttpGet("{count}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeats([FromRoute] GetRepeats.Query query, CancellationToken cancellationToken)
        => new JsonResult(await Mediator.Send(query, cancellationToken));
}