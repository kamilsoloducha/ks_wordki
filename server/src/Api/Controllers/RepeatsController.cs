using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("repeats")]
public class RepeatsController : BaseController
{
    private readonly IHashIdsService _hashIds;

    public RepeatsController(IMediator mediator, IHashIdsService hashIds) : base(mediator)
    {
        _hashIds = hashIds;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(IEnumerable<RepeatDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRepeats([FromQuery] Api.Model.Requests.GetRepeats request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!request.LessonIncluded.HasValue) return BadRequest();

        long? groupId = default;

        if (_hashIds.TryGetLongId(request.GroupId, out var unHashedGroupId))
        {
            groupId = unHashedGroupId;
        }

        var query = new GetRepeats.Query(userId, groupId, request.Count, request.Languages,
            request.LessonIncluded.Value);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }


    [HttpGet("count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetRepeatsCount([FromQuery] Api.Model.Requests.GetRepeatsCount request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var query = new GetRepeatsCount.Query(userId, request.Languages);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}