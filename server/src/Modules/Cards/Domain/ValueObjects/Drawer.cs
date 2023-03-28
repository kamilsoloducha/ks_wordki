using System;

namespace Cards.Domain.ValueObjects;

public class Drawer
{
    public const int MaxValue = 5;

    public int Correct { get; }

    public Drawer() : this(0)
    {
    }

    public Drawer(int correct)
    {
        if (correct < 0) throw new Exception();
        Correct = correct;
    }

    public int Value => Correct + 1 > MaxValue ? MaxValue : Correct + 1;

    public Drawer Increase(int step = 1) => new(Correct + step);
}