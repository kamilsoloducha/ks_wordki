using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.IntegrationEvents;
using Lessons.Domain.Lesson;

namespace Lessons.Domain.Performance;

public class Performance : Entity
{
    public PerformanceId Id { get; private set; }
    public Guid UserId { get; private set; }
    public IList<Lesson.Lesson> Lessons { get; private set; } = new List<Lesson.Lesson>();

    private Performance()
    {
    }

    public static Performance Create(Guid userId)
    {
        return new Performance
        {
            Id = PerformanceId.Create(),
            UserId = userId,
            Lessons = new List<Lesson.Lesson>()
        };
    }

    public DateTime StartLesson(LessonType type)
    {
        var newLesson = Lesson.Lesson.NewLesson(this, type);
        Lessons.Add(newLesson);
        return newLesson.StartDate;
    }

    public void RegisterAnswer(long cardId, int sideType, int result)
    {
        var latestLesson = Lessons.Aggregate((l1, l2) => l1.StartDate > l2.StartDate ? l1 : l2);

        // latestLesson.RegisterAnswer(cardId, side, result);
        _events.Add(new AnswerRegistered
        {
            UserId = UserId,
            CardId = cardId,
            SideType = sideType,
            Result = result
        });
    }
}