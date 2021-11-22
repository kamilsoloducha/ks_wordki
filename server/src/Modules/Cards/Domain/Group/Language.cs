namespace Cards.Domain
{
    public readonly struct Language
    {
        public LanguageType Type { get; }

        private Language(LanguageType type)
        {
            Type = type;
        }

        public static Language Create(LanguageType type) => new Language(type);
    }
}