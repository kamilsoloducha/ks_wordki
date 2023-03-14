using Api.Model.Requests.Models;

namespace Api.Model.Requests;

public record UpdateCard(CardSide Front, CardSide Back, string Comment);