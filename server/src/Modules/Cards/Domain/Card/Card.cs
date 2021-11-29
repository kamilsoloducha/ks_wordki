using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;
using Utils;

namespace Cards.Domain
{
    public class Card : Entity
    {
        public CardId Id { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IEnumerable<CardSide> Sides { get; private set; }
        public Group Group { get; private set; }

        public CardSide Front => Sides.Single(x => x.Side == Side.Front);
        public CardSide Back => Sides.Single(x => x.Side == Side.Back);

        private Card() { }
        internal static Card Create(
                SideLabel frontValue,
                string frontExample,
                bool frontIsUsed,
                SideLabel backValue,
                string backExample,
                bool backIsUsed,
                string comment
            )
        {
            var newCard = new Card
            {
                Id = CardId.Create(),
                Comment = comment,
                CreationDate = SystemClock.Now,
                IsNew = true
            };

            var front = CardSide.CreateFront(newCard, frontValue, frontExample, frontIsUsed);
            var back = CardSide.CreateBack(newCard, backValue, backExample, backIsUsed);

            var sides = new[] { front, back };

            newCard.SetSides(sides);

            return newCard;
        }

        internal void SetSides(IEnumerable<CardSide> sides)
        {
            CheckRule(new SetSidesCardSidesCountRule(sides));
            CheckRule(new SetSidesSidesRule(sides, Side.Front));
            CheckRule(new SetSidesSidesRule(sides, Side.Back));

            Sides = sides;
        }

        internal void UpdateBack(SideLabel value, string example, bool isUsed)
            => UpdateSide(value, example, isUsed, Side.Back);



        internal void UpdateFront(SideLabel value, string example, bool isUsed)
            => UpdateSide(value, example, isUsed, Side.Front);

        private void UpdateSide(SideLabel value, string example, bool isUsed, Side side)
        {
            var cardSide = GetSide(side);
            cardSide.Update(value, example, isUsed);
        }

        internal void UpdateComment(string comment)
        {
            Comment = comment;
        }

        public void RegisterAnswer(Side sideType, int result, INextRepeatCalculator nextRepeatCalculator)
        {
            var side = GetSide(sideType);
            side.RegisterAnswer(nextRepeatCalculator, result);
        }

        internal CardSide GetSide(Side side)
            => side == Side.Front ? Front : Back;
    }
}