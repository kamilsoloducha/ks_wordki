namespace Cards.Application.Queries.Models;

public record RepeatDto(string CardId, int SideType, int QuestionDrawer, string Question, string QuestionExample,
    string QuestionLanguage, string Answer, string AnswerExample, string AnswerLanguage);