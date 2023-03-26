using System;

namespace Cards.Domain.ValueObjects;

public class Counter
{
    public int Value { get; }

    public Counter()
    {
    }

    public Counter(int value)
    {
        if (value < 0) throw new Exception();
        Value = value;
    }

    public Counter Increase() => new (Value + 1);
}