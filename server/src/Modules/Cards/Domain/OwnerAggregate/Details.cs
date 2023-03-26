using System;
using Cards.Domain.Enums;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.Utils;

namespace Cards.Domain.OwnerAggregate;

public class Details : IAggregateRoot
{
    public SideType SideType { get; }
    public virtual Card Card { get; }

    public Drawer Drawer { get; private set; }
    public Counter Counter { get; private set; }
    public bool IsQuestion { get; private set; }
    public bool IsTicked { get; internal set; }
    public DateTime? NextRepeat { get; private set; }

    protected Details()
    {
    }

    public Details(SideType sideType, bool isQuestion, Card card) : this()
    {
        SideType = sideType;
        IsQuestion = isQuestion;
        Drawer = new Drawer();
        Counter = new Counter();
        NextRepeat = isQuestion ? new DateTime().ToUniversalTime() : null;
        Card = card;
    }

    internal void AnswerCorrect(INextRepeatCalculator nextRepeatCalculator)
    {
        IsQuestion = true;
        var increase = GetIncrease();
        Drawer = Drawer.Increase(increase);
        Counter = Counter.Increase();
        NextRepeat = nextRepeatCalculator.Calculate(this, 1);
    }

    internal void AnswerWrong()
    {
        IsQuestion = true;
        Counter = Counter.Increase();
        Drawer = new Drawer();
        NextRepeat = SystemClock.Now.Date.AddDays(1);
    }

    internal void AnswerAccepted()
    {
        IsQuestion = true;
        Counter = Counter.Increase();
        NextRepeat = SystemClock.Now.Date.AddDays(1);
    }

    internal void SetQuestionable(bool isQuestionable)
    {
        if (IsQuestion == isQuestionable)
        {
            return;
        }

        IsQuestion = isQuestionable;
        NextRepeat = IsQuestion ? new DateTime().ToUniversalTime() : null;
    }

    private int GetIncrease() => Drawer.Correct > Counter.Value ? 3 : 1;
}