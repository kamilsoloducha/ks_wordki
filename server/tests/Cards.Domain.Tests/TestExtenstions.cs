namespace Cards.Domain.Tests
{
    public static class TestExtenstions
    {
        public static TSut SetProperty<TSut, TProperty>(this TSut sut, string propertyName, TProperty value)
        {
            sut.GetType().GetProperty(propertyName).SetValue(sut, value, null);
            return sut;
        }
    }
}