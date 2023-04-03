namespace Api.Model.Requests;

// public record SearchCards(string SearchingTerm, IEnumerable<int> SearchingDrawers, bool? IsTicked, bool? LessonIncluded,
//     int PageNumber, int PageSize);
    
    
public record SearchCards
{
    public string? SearchingTerm { get; set; }
    public int?[]? SearchingDrawers { get; set; }
    public bool? IsTicked { get; set; }
    public bool? LessonIncluded { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    