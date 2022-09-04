using NUnit.Framework;
using FluentAssertions;

namespace Cards.Domain.Tests.DetailTests;

[TestFixture]
public class UpdateDetailsTests
{

    [TestCase(true, false)]
    [TestCase(true, true)]
    [TestCase(false, false)]
    [TestCase(false, true)]
    public void UpdateDetailsAndCheckLessonInclude(bool initialValue, bool expectedValue)
    {
        var sut = DetailsBuilder.Default.Build();
        sut.SetProperty(nameof(sut.LessonIncluded), initialValue);

        sut.UpdateDetails(expectedValue, true);

        sut.LessonIncluded.Should().Be(expectedValue);
    }

    [TestCase(true, false)]
    [TestCase(true, true)]
    [TestCase(false, false)]
    [TestCase(false, true)]
    public void UpdateDetailsAndCheckIsTicked(bool initialValue, bool expectedValue)
    {
        var sut = DetailsBuilder.Default.Build();
        sut.SetProperty(nameof(sut.IsTicked), initialValue);

        sut.UpdateDetails(true, expectedValue);

        sut.IsTicked.Should().Be(expectedValue);
    }
}