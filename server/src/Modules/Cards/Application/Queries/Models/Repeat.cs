using System;

namespace Cards.Application.Queries.Models;

public class Repeat
{
    public Guid UserId { get; set; }
    public long CardId { get; set; }
    public int SideType { get; set; }
    public bool LessonIncluded { get; set; }
    public DateTime? NextRepeat { get; set; }
    public int QuestionDrawer { get; set; }
    public string Question { get; set; }
    public string QuestionExample { get; set; }
    public string QuestionLanguage { get; set; }
    public string Answer { get; set; }
    public string AnswerExample { get; set; }
    public string AnswerLanguage { get; set; }
    public long GroupId { get; set; }
}

public class DailyRepeatsCount
{
    public Guid OwnerId { get; set; }
    public int Count { get; set; }
}