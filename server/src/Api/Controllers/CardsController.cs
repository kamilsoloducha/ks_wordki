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
    [Route("cards")]
    public class CardsController : BaseController
    {
        public CardsController(IMediator mediator) : base(mediator) { }


        [HttpGet("repeats/{count}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetRepeats([FromRoute] GetRepeats.Query query, CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(query, cancellationToken));

        [HttpGet("{userId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> GetAll([FromRoute] GetGroups.Query query, CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(query, cancellationToken));

        [HttpGet("{userId}/{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetGroup.Query query, CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(query, cancellationToken));

        [HttpPost("add/group")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPost("add/card")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("update/group")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("update/card")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("add/cardSide")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(AddCardSide.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete/group")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteGroup.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete/card")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);
    }
}