using System;
using Blueprints.Domain;

namespace Cards.Domain
{
    public class CardSide : Entity
    {
        public CardId CardId { get; private set; }
        public string Value { get; private set; }
        public string Example { get; private set; }
        public Drawer Drawer { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? NextRepeat { get; private set; }
        public RepeatCounter Repeats { get; private set; }
        public Side Side { get; private set; }

        public Card Card { get; private set; }
        public CardSide SecondSide => Card.GetSide(Side.GetSecondSide());

        private CardSide() { }

        internal static CardSide CreateFront(Card card, string value, string example)
            => Create(card, value, example, Side.Front);

        internal static CardSide CreateBack(Card card, string value, string example)
            => Create(card, value, example, Side.Back);

        internal void Update(string value, string example, int langauge)
        {
            Value = value;
            Example = example;
        }

        private static CardSide Create(Card card, string value, string example, Side side)
        => new CardSide
        {
            CardId = card.Id,
            Card = card,
            Value = value,
            Example = example,
            IsUsed = false,
            NextRepeat = null,
            Drawer = Drawer.Initial,
            Repeats = RepeatCounter.Initial,
            Side = side,
            IsNew = true
        };

        internal void RegisterAnswer(INextRepeatCalculator nextRepeatCalculator, int result)
        {
            CheckRule(new RegisterAnswerIsUsedRule(IsUsed));

            NextRepeat = nextRepeatCalculator.Calculate(this, result);
            UpdateDrawer(result);
            Repeats.Increse();
        }

        internal void ChangeUsage()
        {
            IsUsed = !IsUsed;
            if (IsUsed)
                NextRepeat = DateTime.Now;
        }

        private void UpdateDrawer(int result)
        {
            if (result > 0)
            {
                Drawer.Increse();
            }
            else
            {
                Drawer.Reset();
            }
        }
    }
}