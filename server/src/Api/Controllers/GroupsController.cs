using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Commands;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : BaseController
    {
        public GroupsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{ownerId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetAll([FromRoute] GetGroupsSummary.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpGet("details/{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetGroupDetails.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpPost("add")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);


        [HttpPost("append")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Append(AppendGroup.Command command, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(command, cancellationToken));

        [HttpPut("update")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("merge")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Merge(MergeGroups.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

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
    }
}