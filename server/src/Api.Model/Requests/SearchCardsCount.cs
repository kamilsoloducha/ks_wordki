namespace Api.Model.Requests;

//public record SearchCardsCount(string SearchingTerm, int?[]? SearchingDrawers, bool? IsTicked, bool? LessonIncluded);

public class SearchCardsCount
{
    public string? SearchingTerm { get; set; }
    public int?[]? SearchingDrawers { get; set; }
    public bool? IsTicked { get; set; }
    public bool? LessonIncluded { get; set; }
}