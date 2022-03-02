namespace Cards.Domain.Tests
{
    public static class TestExtenstions
    {
        public static void SetProperty<TSut, TProperty>(this TSut details, string propertyName, TProperty value)
            => details.GetType().GetProperty(propertyName).SetValue(details, value, null);
    }
}