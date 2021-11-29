using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.SideLabelTests
{
    [TestFixture(typeof(SimpleText))]
    [TestFixture(typeof(SimpleSentence))]
    [TestFixture(typeof(TrimmedSpaces))]
    [TestFixture(typeof(TrimmedTabulators))]
    public class SideLabelCreateTests<TContext> where TContext : SideLabelCreateContext, new()
    {
        private readonly TContext _context = new();

        [Test]
        public void Test()
        {
            var sideLabel = SideLabel.Create(_context.GivenValue);

            sideLabel.Value.Should().BeEquivalentTo(_context.ExpectedValue);
        }
    }

}