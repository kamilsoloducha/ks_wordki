using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class DashboardController : BaseController
{
    public DashboardController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("summary")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(GetDashboardSummary.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var query = new GetDashboardSummary.Query(userId);
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("forecast")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(IEnumerable<RepeatCount>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] Api.Model.Requests.GetForecast request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var query = new GetForecast.Query(userId, request.Count);

        return Ok(await Mediator.Send(query, cancellationToken));
    }
}