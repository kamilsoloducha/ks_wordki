using System;
using Domain.Utils;

namespace Cards.Domain.Services;

public static class RepeatPeriod
{
    public static DateTime To { get; } = SystemClock.Now.Date.Add(new TimeSpan(23, 59, 59));
}