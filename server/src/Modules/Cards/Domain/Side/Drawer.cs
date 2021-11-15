namespace Cards.Domain
{
    public class Drawer : ValueObject
    {
        public static int MaxValue = 5;
        public static int MinValue = 1;
        public static Drawer Initial = Create(1);

        public int Value { get; private set; }
        protected override object GetAtomicValue => Value;

        private Drawer() { }

        public static Drawer Create(int value)
        {
            return new Drawer
            {
                Value = value
            };
        }

        public static Drawer Increse(Drawer drawer)
            => drawer.Value <= MaxValue ? drawer : Create(drawer.Value + 1);
    }
}