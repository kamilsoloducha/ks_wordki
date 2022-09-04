using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class DashboardController : BaseController
{
    public DashboardController(IMediator mediator) : base(mediator) { }

    [HttpGet("summary/{userId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetDashboardSummary.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpGet("forecast")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromQuery] GetForecast.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));
}