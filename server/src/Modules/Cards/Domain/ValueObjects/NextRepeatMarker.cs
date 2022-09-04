using System;

namespace Cards.Domain.ValueObjects;

public readonly struct NextRepeatMarker
{
    public DateTime Date { get; }

    private NextRepeatMarker(DateTime date)
    {
        Date = date;
    }

    public static NextRepeatMarker New() => Restore(DateTime.MinValue);

    public static NextRepeatMarker Restore(DateTime date)
    {
        return new NextRepeatMarker(date.Date);
    }
}