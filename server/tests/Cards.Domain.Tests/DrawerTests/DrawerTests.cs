using NUnit.Framework;
using FluentAssertions;
using System;

namespace Cards.Domain.Tests.DrawerTests
{
    [TestFixture]
    public class DrawerTests
    {

        [Test]
        public void CreateInitailDrawer()
        {
            var initial = Drawer.New();
            initial.Value.Should().Be(1);
            initial.CorrectRepeat.Should().Be(0);

        }

        [Test]
        public void CreateDefaultDrawer()
        {
            const int initValue = 3;
            var drawer = Drawer.Create(initValue);

            drawer.Value.Should().Be(initValue + 1);
            drawer.CorrectRepeat.Should().Be(initValue);
        }

        [Test]
        public void CreateDrawerOverMaxValue()
        {
            const int initValue = 10;
            var drawer = Drawer.Create(initValue);

            drawer.Value.Should().Be(Drawer.MaxValue);
            drawer.CorrectRepeat.Should().Be(initValue);
        }

        [Test]
        public void IncreseDrawer()
        {
            const int initValue = 3;
            var drawer = Drawer.Create(initValue);

            drawer = drawer.Increase();
            drawer.Value.Should().Be(initValue + 2);
            drawer.CorrectRepeat.Should().Be(initValue + 1);
        }

        [Test]
        public void IncreseDrawerByValue()
        {
            const int initValue = 3;
            var drawer = Drawer.Create(initValue);

            drawer = drawer.Increase(2);
            drawer.Value.Should().Be(initValue + 2);
            drawer.CorrectRepeat.Should().Be(initValue + 2);
        }

        [Test]
        public void IncreseDrawerWhenMaxValue()
        {
            var initValue = Drawer.MaxValue;
            var drawer = Drawer.Create(initValue);

            drawer = drawer.Increase();
            drawer.Value.Should().Be(initValue);
            drawer.CorrectRepeat.Should().Be(initValue + 1);
        }

        [Test]
        public void ExceptionWhenValueIsTooSmall()
        {
            const int initValue = -1;
            Action action = () => Drawer.Create(initValue);

            action.Should().Throw<Exception>();
        }


        [Test]
        public void Test()
        {
            var result = Calculate("test2", "test");
            result.Should().Be(1);
        }

        public static int Calculate(string source1, string source2) //O(n*m)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            // return result
            return matrix[source1Length, source2Length];
        }
    }
}