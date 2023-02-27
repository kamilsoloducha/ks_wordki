using System;
using System.Collections.Generic;
using Cards.Domain.Enums;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate;

public class Card
{
    private List<Group> _groups;
    public CardId Id { get; private set; }
    public Side Front { get; private set; }
    public Side Back { get; private set; }
    public bool IsPrivate { get; internal set; }
    public IReadOnlyList<Group> Groups => _groups.AsReadOnly();

    public SideId FrontId { get; private set; }
    public SideId BackId { get; private set; }

    private Card()
    {
        _groups = new();
    }

    public static Card New(Label frontValue, Label backValue, Example frontExample, Example backExample, ISequenceGenerator sequenceGenerator)
    {
        var cardId = CardId.New(sequenceGenerator);
        var front = Side.New(cardId, SideType.Front, frontValue, frontExample, sequenceGenerator);
        var back = Side.New(cardId, SideType.Back, backValue, backExample, sequenceGenerator);
        return new Card
        {
            Id = cardId,
            Front = front,
            Back = back,
            IsPrivate = true,
            BackId = back.Id,
            FrontId = front.Id
        };
    }

    internal void Update(Label frontValue, Label backValue, Example frontExample, Example backExample)
    {
        if (!IsPrivate) throw new Exception("Can not update public card");
        Front.Value = frontValue;
        Front.Example = frontExample;
        
        Back.Value = backValue;
        Back.Example = backExample;
        
    }
}