namespace Cards.Domain.ValueObjects;

public class Example
{
    public string Value { get; private init; }

    public Example(string value)
    {
        Value = value.Trim();
    }
}