namespace Cards.Domain
{
    public readonly struct RepeatCounter
    {
        public static RepeatCounter Initial = Create(MinValue);
        private const int MinValue = 0;
        public int Value { get; }

        internal RepeatCounter(int value)
        {
            Value = value;
        }

        public static RepeatCounter Create(int value)
        {
            if (value < MinValue)
            {
                throw new System.Exception("value < 0");
            }

            return new RepeatCounter(value);
        }

        public RepeatCounter Increse()
            => Create(Value + 1);
    }
}