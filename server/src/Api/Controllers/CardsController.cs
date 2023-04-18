using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Cards.Application.Features.Cards;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("cards")]
public class CardsController : BaseController
{
    private readonly IHashIdsService _hashIds;

    public CardsController(IMediator mediator, IHashIdsService hashIds) : base(mediator)
    {
        _hashIds = hashIds;
    }

    [HttpPost("add/{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromRoute] string groupId, [FromBody] Model.Requests.AddCard request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var addCardCommand = new AddCard.Command(
            userId,
            unHashedGroupId,
            new AddCard.CardSide(request.Front.Value, request.Front.Example, request.Front.IsUsed),
            new AddCard.CardSide(request.Back.Value, request.Back.Example, request.Back.IsUsed),
            request.Comment);

        var result = await Mediator.Send(addCardCommand, cancellationToken);

        if (!result.IsCorrect)
        {
            return BadRequest(result.Error);
        }

        var cardId = _hashIds.GetHash(result.Response);
        return Created($"{groupId}/{cardId}", cardId);
    }

    [HttpPost("add/extension")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Add(Model.Requests.AddCardFromExtension request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var addCardCommand = new AddCardExtenstion.Command(userId, request.Value);

        var result = await Mediator.Send(addCardCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpPost("add/file/{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Add([FromRoute] string groupId, Model.Requests.AddCardsFromFile request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var addCardsCommand = new AddCardsFromFile.Command(userId, unHashedGroupId, request.Content,
            request.ItemSeparator, request.ElementSeparator, request.ItemsOrder);

        var result = await Mediator.Send(addCardsCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }


    [HttpPut("update/{cardId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> Update(
        [FromRoute] string cardId,
        Model.Requests.UpdateCard request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();

        var updateCardCommand = new UpdateCard.Command(userId, unHashedCardId,
            new UpdateCard.CardSide(request.Front.Value, request.Front.Example, request.Front.IsUsed,
                request.Front.IsTicked),
            new UpdateCard.CardSide(request.Back.Value, request.Back.Example, request.Back.IsUsed,
                request.Back.IsTicked),
            request.Comment);

        var result = await Mediator.Send(updateCardCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("{cardId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] string cardId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();

        var deleteCardCommand = new DeleteCard.Command(userId, unHashedCardId);

        var result = await Mediator.Send(deleteCardCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("tick/{cardId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Tick([FromRoute] string cardId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();

        var tickCommand = new TickCard.Command(userId, unHashedCardId);

        var result = await Mediator.Send(tickCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpGet("summaries/{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(IEnumerable<CardSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSummaries([FromRoute] string groupId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();
        
        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var query = new GetCardSummaries.Query(userId, unHashedGroupId);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("summary/{cardId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    [ProducesResponseType(typeof(CardSummaryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSummary([FromRoute] string cardId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();
        
        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();
        
        var query = new GetCardSummary.Query(userId, unHashedCardId);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }


    [HttpGet("search")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromQuery] Model.Requests.SearchCards request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var query = new SearchCards.Query(userId, request.SearchingTerm,
            request.SearchingDrawers?.Where(x => x.HasValue).Select(x => x.Value) ?? Enumerable.Empty<int>(),
            request.LessonIncluded, request.IsTicked, request.PageNumber, request.PageSize);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }
        

    [HttpGet("search/count")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromQuery] Model.Requests.SearchCardsCount request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var query = new SearchCardsCount.Query(userId, request.SearchingTerm,
            request.SearchingDrawers?.Where(x => x.HasValue).Select(x => x.Value) ?? Enumerable.Empty<int>(),
            request.LessonIncluded, request.IsTicked);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("overview/{ownerId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetCardsOverview.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));
}