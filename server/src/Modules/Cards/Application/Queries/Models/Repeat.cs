using System;

namespace Cards.Application.Queries
{

    public class Repeat
    {
        public Guid CardId { get; set; }
        public string QuestionValue { get; set; }
        public string QuestionExample { get; set; }
        public int QuestionSide { get; set; }
        public DateTime NextRepeat { get; set; }
        public string AnswerValue { get; set; }
        public string AnswerExample { get; set; }
        public int AnswerSide { get; set; }
        public string Comment { get; set; }
        public int FrontLanguage { get; set; }
        public int BackLanguage { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class DailyRepeatsCount
    {
        public Guid UserId { get; set; }
        public int Count { get; set; }
    }
}