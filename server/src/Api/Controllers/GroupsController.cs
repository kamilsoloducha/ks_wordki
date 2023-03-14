using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Cards.Application.Features.Groups;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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

            var addGroupCommand = new AddGroup.Command(userId, request.Name, request.Front, request.Back);
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

        [HttpGet("forlesson")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        [ProducesResponseType(typeof(IEnumerable<GroupToLessonDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

            var query = new GetGroupsForLearn.Query(userId);

            var response = await Mediator.Send(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet("summaries")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

            var query = new GetGroupSummaries.Query(userId);

            var result = await Mediator.Send(query, cancellationToken);
        
            return Ok(result);
        }

        [HttpGet("summary/{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        [ProducesResponseType(typeof(GroupSummaryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string groupId, CancellationToken cancellationToken)
        {
            if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

            if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
                return BadRequest();
        
            var query = new GetGroupSummary.Query(userId, unHashedGroupId);

            var result = await Mediator.Send(query, cancellationToken);

            return result is not null ? Ok(result) : BadRequest("Group has not found");
        }

        [HttpPut("search")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Put([FromBody] SearchGroups.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpPut("search/count")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Put([FromBody] SearchGroupsCount.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpGet("languages")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetLanguages(CancellationToken cancellationToken)
        {
            if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

            var query = new GetLanguages.Query(userId);

            var result = await Mediator.Send(query, cancellationToken);
            
            return Ok(result);
        }

    


    
    }
}