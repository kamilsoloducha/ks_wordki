namespace Cards.Domain
{
    public enum Side
    {
        Front,
        Back
    }

    public class RepeatCounter
    {
        private const int MinValue = 0;
        public int Value { get; private set; }
        public static RepeatCounter Initial = Create(MinValue);

        private RepeatCounter() { }

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

        public void Increse()
        {
            Value++;
        }
    }
}