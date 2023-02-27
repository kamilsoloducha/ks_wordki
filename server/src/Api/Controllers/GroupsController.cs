using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Cards.Application.Features.Groups;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("groups")]
public class GroupsController : BaseController
{
    private readonly IHashIdsService _hashIds;

    public GroupsController(IMediator mediator, IHashIdsService hashIds) : base(mediator)
    {
        _hashIds = hashIds;
    }

    [HttpPost("add")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Add(Model.Requests.AddGroup request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var addGroupCommand = new AddGroup.Command(userId, request.Name, request.Back, request.Front);
        var response = await Mediator.Send(addGroupCommand, cancellationToken);

        if (!response.IsCorrect) return BadRequest(response.Error);

        var hashedGroupId = _hashIds.GetHash(response.Response);
        return Created($"/groups/details/{hashedGroupId}", hashedGroupId);
    }
    
    [HttpPut("update/{groupId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Update([FromRoute] string groupId, [FromBody] Model.Requests.UpdateGroup request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var command = new UpdateGroup.Command(userId, unHashedGroupId, request.Name, request.Front, request.Back);

        var response = await Mediator.Send(command, cancellationToken);

        return response.IsCorrect
            ? NoContent()
            : BadRequest(response.Error);
    }
    
    [HttpDelete("{groupId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Delete([FromRoute] string groupId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var response = await Mediator.Send(new DeleteGroup.Command(userId, unHashedGroupId), cancellationToken);

        return response.IsCorrect ? NoContent() : BadRequest(response.Error);
    }
    
    [HttpPost("attach")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Attach(Model.Requests.AttachGroup requests, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(requests.GroupId, out var unHashedGroupId))
            return BadRequest();

        var attachGroupCommand = new AttachGroup.Command(userId, unHashedGroupId);
        var result = await Mediator.Send(attachGroupCommand, cancellationToken);

        var hashedGroupId = _hashIds.GetHash(result);
        return Created($"/groups/details/{hashedGroupId}", hashedGroupId);
    }

    [HttpGet("dashboard/summary")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
        => Ok(await Mediator.Send(new GetDashboardSummary.Query(), cancellationToken));

    [HttpGet("lesson/{ownerId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetGroupsForLearn.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpPut("search")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Put([FromBody] SearchGroups.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpPut("search/count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Put([FromBody] SearchGroupsCount.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));
    
    [HttpGet("{ownerId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetAll([FromRoute] GetGroupsSummary.Query query,
        CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpGet("details/{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetGroupDetails.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));


    
}