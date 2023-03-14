namespace Cards.Application.Queries.Models;

public record CardSummaryDto(string Id, SideSummaryDto Front, SideSummaryDto Back);