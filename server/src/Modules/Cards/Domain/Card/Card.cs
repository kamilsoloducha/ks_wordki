using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;

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
                string frontValue,
                string frontExample,
                string backValue,
                string backExample,
                string comment
            )
        {
            var newCard = new Card
            {
                Id = CardId.Create(),
                Comment = comment,
                CreationDate = DateTime.Now,
                IsNew = true
            };

            var front = CardSide.CreateFront(newCard, frontValue, frontExample);
            var back = CardSide.CreateBack(newCard, backValue, backExample);

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

        internal void UpdateBack(string value, string example, int language)
            => UpdateSide(value, example, language, Side.Back);



        internal void UpdateFront(string value, string example, int language)
            => UpdateSide(value, example, language, Side.Front);

        private void UpdateSide(string value, string example, int langauge, Side side)
        {
            var cardSide = GetSide(side);
            cardSide.Update(value, example, langauge);
        }

        internal void UpdateComment(string comment)
        {
            Comment = comment;
        }

        internal void RegisterAnswer(Side sideType, int result, INextRepeatCalculator nextRepeatCalculator)
        {
            var side = GetSide(sideType);
            side.RegisterAnswer(nextRepeatCalculator, result);
        }

        internal CardSide GetSide(Side side)
            => side == Side.Front ? Front : Back;
    }
}