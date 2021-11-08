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

        [HttpPost("add")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("update")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("enable")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(AddCardSide.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete(DeleteCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpGet("dashboard/summary")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
            => new JsonResult(await Mediator.Send(new GetDashboardSummary.Query(), cancellationToken));
    }
}