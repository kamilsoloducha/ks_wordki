namespace Cards.Domain
{
    public readonly struct SideLabel
    {
        public string Value { get; }
        private SideLabel(string value)
        {
            Value = value;
        }

        public static SideLabel Create(string labal)
        {
            var trimmedLabel = labal.Trim();
            if (string.IsNullOrEmpty(trimmedLabel))
            {
                throw new System.Exception("Side label is required");
            }

            return new SideLabel(trimmedLabel);
        }
    }
}