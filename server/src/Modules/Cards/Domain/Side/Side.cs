namespace Cards.Domain
{
    public enum Side
    {
        Front,
        Back
    }

    public class RepeatCounter : ValueObject
    {
        public static RepeatCounter Initial = Create(MinValue);
        private const int MinValue = 0;
        public int Value { get; private set; }
        protected override object GetAtomicValue => Value;

        protected RepeatCounter() { }

        public static RepeatCounter Create(int value)
        {
            if (value < MinValue)
            {
                throw new System.Exception("value < 0");
            }

            return new RepeatCounter
            {
                Value = value,
            };
        }

        public RepeatCounter Increse()
            => Create(Value + 1);
    }
}