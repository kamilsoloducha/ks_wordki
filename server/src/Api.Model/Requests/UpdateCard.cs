using Api.Model.Requests.Models;

namespace Api.Model.Requests;

public record UpdateCard(Guid UserId, long CardId, CardSide Front, CardSide Back, string Comment);