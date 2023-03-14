namespace Cards.Application.Queries.Models;

public record SideSummaryDto(int Type, string Value, string Example, string Comment, int Drawer, bool IsUsed,
    bool IsTicked);