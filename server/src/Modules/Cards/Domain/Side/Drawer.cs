namespace Cards.Domain
{
    public readonly struct Drawer
    {
        public static int MaxValue = 5;
        public static int MinValue = 1;
        public static Drawer Initial = Create(1);

        public int Value { get; }

        private Drawer(int value)
        {
            Value = value;
        }

        public static Drawer Create(int value)
        {
            if (value < MinValue || value > MaxValue)
            {
                throw new System.Exception("Drawer value exceeded valid range");
            }
            return new Drawer(value);
        }

        public static Drawer Increse(Drawer drawer)
            => drawer.Value < MaxValue ? Create(drawer.Value + 1) : drawer;
    }
}