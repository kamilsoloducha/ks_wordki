namespace Cards.Domain
{
    public readonly struct Drawer
    {
        public const int MaxValue = 5;
        public const int MinValue = 1;
        public static readonly Drawer Initial = Create(1);

        public int Value { get; }
        public int RealValue { get; }

        private Drawer(int value, int realValue)
        {
            Value = value;
            RealValue = realValue;
        }

        public static Drawer Create(int realValue)
        {
            if (realValue < MinValue)
            {
                throw new System.Exception("Drawer value exceeded valid range");
            }
            var value = realValue > MaxValue ? MaxValue : realValue;
            return new Drawer(value, realValue);
        }

        public Drawer Increse()
            => Create(RealValue + 1);
    }
}