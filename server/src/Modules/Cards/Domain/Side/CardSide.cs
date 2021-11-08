using System;
using Blueprints.Domain;

namespace Cards.Domain
{

    public class NextRepeat
    {
        public static readonly NextRepeat NullValue = Restore(null);
        public DateTime? Value { get; private set; }

        private NextRepeat() { }

        public static NextRepeat Restore(DateTime? dateTime)
            => dateTime.HasValue
            ? new NextRepeat { Value = dateTime?.Date }
            : NextRepeat.NullValue;

        public static NextRepeat Create()
                => new NextRepeat
                {
                    Value = DateTime.Now.Date
                };

    }

    public class CardSide : Entity
    {
        public CardId CardId { get; private set; }
        public string Value { get; private set; }
        public string Example { get; private set; }
        public Drawer Drawer { get; private set; }
        public bool IsUsed { get; private set; }
        public NextRepeat NextRepeat { get; private set; }
        public RepeatCounter Repeats { get; private set; }
        public Side Side { get; private set; }

        public Card Card { get; private set; }
        public CardSide SecondSide => Card.GetSide(Side.GetSecondSide());

        private CardSide() { }

        internal static CardSide CreateFront(Card card, string value, string example, bool isUsed)
            => Create(card, value, example, isUsed, Side.Front);

        internal static CardSide CreateBack(Card card, string value, string example, bool isUsed)
            => Create(card, value, example, isUsed, Side.Back);

        internal void Update(string value, string example, bool isUsed)
        {
            Value = value;
            Example = example;
            IsUsed = isUsed;
            if (IsUsed && NextRepeat == NextRepeat.NullValue)
                NextRepeat = NextRepeat.Create();
        }

        private static CardSide Create(Card card, string value, string example, bool isUsed, Side side)
        => new CardSide
        {
            CardId = card.Id,
            Card = card,
            Value = value,
            Example = example,
            IsUsed = isUsed,
            NextRepeat = isUsed ? NextRepeat.Create() : NextRepeat.NullValue,
            Drawer = Drawer.Initial,
            Repeats = RepeatCounter.Initial,
            Side = side,
            IsNew = true
        };

        internal void RegisterAnswer(INextRepeatCalculator nextRepeatCalculator, int result)
        {
            CheckRule(new RegisterAnswerIsUsedRule(IsUsed));

            var nextRepatDate = nextRepeatCalculator.Calculate(this, result);
            NextRepeat = NextRepeat.Restore(nextRepatDate);
            // UpdateDrawer(result);
            Drawer = Drawer.Create(Drawer.Value);
            Repeats = RepeatCounter.Create(Repeats.Value + 1);
        }

        internal void ChangeUsage()
        {
            IsUsed = !IsUsed;
            if (IsUsed)
                NextRepeat = NextRepeat.Create();
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