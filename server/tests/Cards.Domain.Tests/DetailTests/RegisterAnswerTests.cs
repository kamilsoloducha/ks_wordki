using NUnit.Framework;
using FakeItEasy;
using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using FluentAssertions;
using Moq;
using Times = Moq.Times;

namespace Cards.Domain.Tests.DetailTests;

[TestFixture]
public class RegisterAnswerTests
{
    private Details sut;
    private INextRepeatCalculator nextRepeatCalculatorMock;


    [SetUp]
    public void Setup()
    {
        sut = DetailsBuilder.Default.Build();
        sut.SetProperty(nameof(sut.Counter), new Counter(1));
        sut.SetProperty(nameof(sut.Drawer), new Drawer());

        nextRepeatCalculatorMock = A.Fake<INextRepeatCalculator>();
        A.CallTo(() => nextRepeatCalculatorMock.Calculate(sut, A<int>._)).Returns(new DateTime(2022, 2, 1));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void RegisterAnswerAndCheckLessonInclude(bool initialLessonInclude)
    {
    }


    [TestCase(1)]
    [TestCase(10)]
    public void RegisterAnswerAndCheckCounter(int initialCounter)
    {
        sut.SetProperty(nameof(sut.Counter), new Counter(initialCounter));

        sut.AnswerAccepted();

        sut.Counter.Value.Should().Be(initialCounter + 1);
    }


    [TestCase("2022/01/01")]
    [TestCase("2022/01/15")]
    public void RegisterAnswerAndCheckNextRepeat(DateTime retrunNextRepeat)
    {
        A.CallTo(() => nextRepeatCalculatorMock.Calculate(sut, A<int>._)).Returns(retrunNextRepeat);
    }

    [TestCase(0, 0, 0, 1)]
    [TestCase(3, 3, 3, 4)]
    [TestCase(10, 20, 10, 5)]
    public void AcceptAnswerShouldUpdateDrawerAndCounter(int initialCorrect, int initialCounter, int expectedCorrect,
        int expectedDrawer)
    {
        SystemClock.Override(new DateTime(2022, 2, 20, 12, 0, 0));
        sut.SetProperty(nameof(sut.Drawer), new Drawer(initialCorrect));
        sut.SetProperty(nameof(sut.Counter), new Counter(initialCounter));
        sut.SetProperty(nameof(sut.NextRepeat), new DateTime(2022, 2, 20));

        sut.AnswerAccepted();

        sut.Drawer.Value.Should().Be(expectedDrawer);
        sut.Drawer.Correct.Should().Be(expectedCorrect);
        sut.Counter.Value.Should().Be(initialCounter + 1);
        sut.NextRepeat.Should().Be(new DateTime(2022, 2, 21));
    }

    [TestCase(0, 0, 1, 2)]
    [TestCase(0, 10, 1, 2)]
    [TestCase(1, 1, 2, 3)]
    [TestCase(1, 2, 2, 3)]
    [TestCase(10, 10, 11, 5)]
    public void CorrectAnswerShouldUpdateDrawerAndCounter(int initialCorrect, int initialCounter, int expectedCorrect,
        int expectedDrawer)
    {
        var nextRepeatCalculator = new Mock<INextRepeatCalculator>();
        nextRepeatCalculator.Setup(x => x.Calculate(sut, 1)).Returns(new DateTime(2022, 2, 21));
        sut.SetProperty(nameof(sut.Drawer), new Drawer(initialCorrect));
        sut.SetProperty(nameof(sut.Counter), new Counter(initialCounter));
        sut.SetProperty(nameof(sut.NextRepeat), new DateTime(2022, 2, 20));

        sut.AnswerCorrect(nextRepeatCalculator.Object);

        sut.Drawer.Value.Should().Be(expectedDrawer);
        sut.Drawer.Correct.Should().Be(expectedCorrect);
        sut.Counter.Value.Should().Be(initialCounter + 1);
        sut.NextRepeat.Should().Be(new DateTime(2022, 2, 21));
        nextRepeatCalculator.Verify(x => x.Calculate(sut, 1), Times.Once);
    }
}