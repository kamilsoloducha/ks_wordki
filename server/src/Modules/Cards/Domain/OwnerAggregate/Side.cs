using Cards.Domain.Enums;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate;

public class Side
{
    public SideId Id { get; private init; }
    public SideType Type { get; private init; }
    public Label Value { get; internal set; }
    public Example Example { get; internal set; }

    private Side()
    { }

    public static Side New(
        CardId cardId,
        SideType type,
        Label value,
        Example example,
        ISequenceGenerator sequenceGenerator)
    {
        return new Side
        {
            Id = SideId.New(sequenceGenerator),
            Type = type,
            Value = value,
            Example = example,
        };
    }
}