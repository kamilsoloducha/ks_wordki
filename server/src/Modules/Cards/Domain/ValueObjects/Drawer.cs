using Blueprints.Domain;

namespace Cards.Domain
{
    public readonly struct Drawer
    {
        private const int MinValue = 0;
        public const int MaxValue = 5;
        public int Value { get; }
        public int CorrectRepeat { get; }

        private Drawer(int correctRepeat)
        {
            CorrectRepeat = correctRepeat;
            Value = correctRepeat + 1 >= MaxValue ? MaxValue : correctRepeat + 1;
        }

        public static Drawer New() => Create(MinValue);

        public static Drawer Create(int correctRepeat)
        {
            if (correctRepeat < 0) throw new BuissnessArgumentException(nameof(correctRepeat), correctRepeat);

            return new Drawer(correctRepeat);
        }

        public Drawer Increase(int step = 1)
            => Create(CorrectRepeat + step);
    }
}