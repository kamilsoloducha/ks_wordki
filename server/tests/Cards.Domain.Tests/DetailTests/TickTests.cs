using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.DetailTests
{
    [TestFixture]
    public class TickTests
    {

        [TestCase(true)]
        [TestCase(false)]
        public void Tick(bool initialIsTicked)
        {
            var details = DetailsBuilder.Default.Build();
            details.SetProperty(nameof(details.IsTicked), initialIsTicked);

            details.Tick();

            details.IsTicked.Should().Be(true);
        }
    }
}