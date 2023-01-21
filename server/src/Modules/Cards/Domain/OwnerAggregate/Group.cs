using System.Collections.Generic;
using System.Linq;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain;

namespace Cards.Domain.OwnerAggregate;

public class Group
{
    private readonly List<Card> _cards;
    public GroupId Id { get; private set; }
    public GroupName Name { get; private set; }
    public Language Front { get; private set; }
    public Language Back { get; private set; }

    public IReadOnlyCollection<Card> Cards => _cards;
    public OwnerId OwnerId { get; private set; }
    public Owner Owner { get; private set; }

    private Group()
    {
        _cards = new();
    }

    internal static Group New(GroupName name, Language front, Language back, ISequenceGenerator sequenceGenerator)
        => new Group()
        {
            Id = GroupId.New(sequenceGenerator),
            Name = name,
            Front = front,
            Back = back
        };

    internal void Update(GroupName name, Language front, Language back)
    {
        Name = name;
        Front = front;
        Back = back;
    }

    internal void RemoveCard(CardId cardId)
    {
        var card = Cards.FirstOrDefault(x => x.Id == cardId);
        if (card is null) return;
        _cards.Remove(card);
    }

    internal void RemoveCard(Card card)
    {
        var result = _cards.Remove(card);
        if (!result) throw new BuissnessArgumentException(nameof(card), card.Id);
    }

    internal Card AddCard(
        Label frontValue,
        Label backValue,
        Example frontExample,
        Example backExample,
        ISequenceGenerator sequenceGenerator)
    {
        var card = Card.New(
            frontValue,
            backValue,
            frontExample,
            backExample,
            sequenceGenerator);

        _cards.Add(card);

        return card;
    }

    internal Card AddCard(Card card)
    {
        _cards.Add(card);
        return card;
    }

    internal Card GetCard(CardId cardId)
    {
        var card = Cards.FirstOrDefault(x => x.Id == cardId);
        if (card is null) throw new BuissnessObjectNotFoundException(nameof(card), cardId);

        return card;
    }
}