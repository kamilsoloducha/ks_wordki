using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.Tests.CardTests;

internal static class Builder
{
    public static Card SampleCard()
    {
        var card =  new Card(
            new Label("frontValue"),
            new Label("frontValue"),
            new Example("frontExample"),
            new Example("frontExample"),
            Activator.CreateInstance(typeof(Group), true) as Group);
        
        card.SetProperty(nameof(card.Id), 2);
        card.Front.SetProperty(nameof(card.Front.Id), 2);
        card.Back.SetProperty(nameof(card.Back.Id), 3);

        card.FrontDetails.SetProperty(nameof(card.FrontDetails.Counter), new Counter(10));
        card.BackDetails.SetProperty(nameof(card.BackDetails.Counter), new Counter(10));

        return card;
    }
}