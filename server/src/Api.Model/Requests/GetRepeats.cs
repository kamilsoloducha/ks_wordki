namespace Api.Model.Requests;

public class GetRepeats
{
    public string? GroupId { get; set; }
    public int? Count { get; set; }
    public string[] Languages { get; set; } = Array.Empty<string>();
    public bool? LessonIncluded { get; set; }
}