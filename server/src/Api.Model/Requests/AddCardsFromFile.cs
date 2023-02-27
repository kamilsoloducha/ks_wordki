namespace Api.Model.Requests;

public record AddCardsFromFile(string Content, string ItemSeparator, string ElementSeparator, string[] ItemsOrder);