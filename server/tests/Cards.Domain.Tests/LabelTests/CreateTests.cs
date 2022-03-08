using System;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.LabelTests
{
    [TestFixture]
    public class CreateTests
    {
        [TestCase("test", "test")]
        [TestCase("test test", "test test")]
        [TestCase(" test test ", "test test")]
        [TestCase("\n test test \n", "test test")]
        [TestCase("\t test test \t", "test test")]
        public void CreateSuccess(string parameter, string expectedText)
        {
            var label = Label.Create(parameter);
            label.Text.Should().Be(expectedText);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("\n")]
        [TestCase("\t")]
        public void CreateFailed(string parameter)
        {
            Action action = () => Label.Create(parameter);
            action.Should().Throw<ArgumentException>();
        }
    }
}