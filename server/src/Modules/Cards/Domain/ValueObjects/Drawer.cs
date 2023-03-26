namespace Cards.Domain.ValueObjects;

public class Drawer
{
    private const int MinValue = 1;
    public const int MaxValue = 5;
    
    public int Correct { get; }
    
    public Drawer() : this(MinValue)
    {
    }

    public Drawer(int correct)
    {
        Correct = correct;
    }
    
    public int Value => Correct > MaxValue ? MaxValue : Correct;
    
    public Drawer Increase(int step = 1) => new (Correct + step);
}