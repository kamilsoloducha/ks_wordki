using System;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.DeleteCard
{
    public abstract class DeleteCardContext
    {
        public Owner GivenOwner { get; }
        public Card GivenCard { get; }
        public virtual long GivenCardId => GivenCard.Id;

        protected DeleteCardContext()
        {
            var owner = DataBuilder.SampleUser().Build();
            var group = DataBuilder.SampleGroup().Build();
            owner.Groups.Add(group);

            GivenCard = DataBuilder.SampleCard().Build();
            group.Cards.Add(GivenCard);

            GivenOwner = owner;
        }

        public virtual int ExpectedSideCount => 2;
        public virtual int ExpectedCardsCount => 1;
        public virtual int ExpectedGroupsCount => 1;
        public virtual int ExpectedDetailsCount => 0;
    }
}