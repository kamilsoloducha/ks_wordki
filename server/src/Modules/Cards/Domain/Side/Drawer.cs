namespace Cards.Domain
{
    public class Drawer
    {
        public static int MaxValue = 5;
        public static int MinValue = 1;
        public static Drawer Initial = Create(1);

        public int Value { get; private set; }
        private Drawer() { }

        public static Drawer Create(int value)
        {
            return new Drawer
            {
                Value = value
            };
        }

        internal void Increse()
        {
            if (Value < MaxValue)
                Value++;
        }

        internal void Reset()
        {
            Value = MinValue;
        }


    }
}