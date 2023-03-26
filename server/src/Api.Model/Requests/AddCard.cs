using Api.Model.Requests.Models;

namespace Api.Model.Requests;

public record AddCard(CardSide Front, CardSide Back, string Comment);

public class Translate
{
    public string Phrase { get; set; }
}