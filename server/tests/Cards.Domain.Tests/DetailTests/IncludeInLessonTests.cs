using NUnit.Framework;
using FluentAssertions;

namespace Cards.Domain.Tests.DetailTests
{
    [TestFixture]
    public class IncludeInLessonTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void IncludeInLessonAndCheckLessonInclude(bool initialValue)
        {
            var sut = DetailsBuilder.Default.Build();
            sut.SetProperty(nameof(sut.LessonIncluded), initialValue);

            sut.IncludeInLesson();

            sut.LessonIncluded.Should().BeTrue();
        }
    }
}