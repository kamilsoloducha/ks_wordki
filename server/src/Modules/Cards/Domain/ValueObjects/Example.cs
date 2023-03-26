namespace Cards.Domain.ValueObjects;

public class Example
{
    public string Text { get; }

    public Example()
    {
    }

    public Example(string value) : this()
    {
        Text = value.Trim();
    }
}