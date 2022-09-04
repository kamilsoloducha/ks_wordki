using NUnit.Framework;
using FakeItEasy;
using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentAssertions;

namespace Cards.Domain.Tests.DetailTests;

[TestFixture]
public class RegisterAnswerTests
{
    private Detail sut;
    private INextRepeatCalculator nextRepeatCalculatorMock;


    [SetUp]
    public void Setup()
    {
        sut = DetailsBuilder.Default.Build();
        sut.SetProperty(nameof(sut.Counter), 1);
        sut.SetProperty(nameof(sut.Drawer), Drawer.Create(0));
        sut.SetProperty(nameof(sut.NextRepeat), NextRepeatMarker.Restore(new DateTime(2022, 1, 1)));
        sut.SetProperty(nameof(sut.LessonIncluded), true);

        nextRepeatCalculatorMock = A.Fake<INextRepeatCalculator>();
        A.CallTo(() => nextRepeatCalculatorMock.Calculate(sut, A<int>._)).Returns(new DateTime(2022, 2, 1));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void RegisterAnswerAndCheckLessonInclude(bool initialLessonInclude)
    {
        sut.SetProperty(nameof(sut.LessonIncluded), initialLessonInclude);

        sut.RegisterAnswer(1, nextRepeatCalculatorMock);

        sut.LessonIncluded.Should().Be(true);
    }


    [TestCase(1)]
    [TestCase(10)]
    public void RegisterAnswerAndCheckCounter(int initialCounter)
    {
        sut.SetProperty(nameof(sut.Counter), initialCounter);

        sut.RegisterAnswer(1, nextRepeatCalculatorMock);

        sut.Counter.Should().Be(initialCounter + 1);
    }


    [TestCase("2022/01/01")]
    [TestCase("2022/01/15")]
    public void RegisterAnswerAndCheckNextRepeat(DateTime retrunNextRepeat)
    {
        A.CallTo(() => nextRepeatCalculatorMock.Calculate(sut, A<int>._)).Returns(retrunNextRepeat);

        sut.RegisterAnswer(1, nextRepeatCalculatorMock);

        sut.NextRepeat.Date.Should().Be(retrunNextRepeat);
    }

    [TestCase(0, 0, 1, 3, 2)]
    [TestCase(0, 1, 1, 2, 1)]
    [TestCase(2, 1, 1, 5, 4)]
    [TestCase(1, 0, 1, 3, 2)]
    [TestCase(2, 2, 1, 4, 3)]
    [TestCase(10, 10, 1, 5, 11)]

    [TestCase(0, 0, 0, 1, 0)]
    [TestCase(1, 0, 0, 2, 1)]
    [TestCase(10, 20, 0, 5, 10)]

    [TestCase(0, 0, -1, 1, 0)]
    [TestCase(1, 1, -1, 1, 0)]
    [TestCase(10, 10, -1, 1, 0)]
    public void RegisterAnswerAndDrawer(int initialValue, int initialCounter, int givenResult, int expectedValue, int expectedCorrectRepeat)
    {
        sut.SetProperty(nameof(sut.Drawer), Drawer.Create(initialValue));
        sut.SetProperty(nameof(sut.Counter), initialCounter);

        sut.RegisterAnswer(givenResult, nextRepeatCalculatorMock);

        sut.Drawer.Value.Should().Be(expectedValue);
        sut.Drawer.CorrectRepeat.Should().Be(expectedCorrectRepeat);
    }
}