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

        [HttpGet("{userId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetAll([FromRoute] GetGroups.Query query, CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(query, cancellationToken));

        [HttpGet("{userId}/{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetGroup.Query query, CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(query, cancellationToken));

        [HttpPost("add")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("update")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpGet("dashboard/summary")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(new GetDashboardSummary.Query(), cancellationToken));
    }
}