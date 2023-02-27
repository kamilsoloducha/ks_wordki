using System;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class Repeat
    {
        public double? Random { get; set; }
        public Guid? OwnerId { get; set; }
        public long? SideId { get; set; }
        public long? CardId { get; set; }
        public bool? LessonIncluded { get; set; }
        public DateTime? NextRepeat { get; set; }
        public int? QuestionDrawer { get; set; }
        public string Question { get; set; }
        public string QuestionExample { get; set; }
        public int? QuestionType { get; set; }
        public int? QuestionLanguage { get; set; }
        public string Answer { get; set; }
        public string AnswerExample { get; set; }
        public int? AnswerType { get; set; }
        public int? AnswerLanguage { get; set; }
        public string Comment { get; set; }
        public string GroupName { get; set; }
        public int? FrontLanguage { get; set; }
        public int? BackLanguage { get; set; }
        public long? GroupId { get; set; }
    }
}
