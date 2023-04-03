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

    public int Value => ToValue(Correct);

    public Drawer Increase(int step = 1) => new(Correct + step);
    
    public static int ToValue(int correct) => correct + 1 > MaxValue ? MaxValue : correct + 1;
}