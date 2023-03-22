using System;

namespace Cards.Domain.ValueObjects;

public class NextRepeatMarker
{
    public DateTime Date { get; }

    public NextRepeatMarker(DateTime date)
    {
        Date = date.Date;
    }
}