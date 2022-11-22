using System.Collections.Generic;
using System.Linq;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class MathTests
    {
        Math math;

        public MathTests()
        {
            this.math = new Math();
        }

        [Fact(Skip = "Because i test skipping atribute.")]
        public void Add_AddsTwoPositiveNumbers_ReturnsTrueValue()
        {
            // Act
            int result = math.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 1, 1)]
        public void Max_WhenCalled_ReturnsGreaterArgument(int a, int b, int c)
        {
            // Act
            int result = math.Max(a, b);

            // Assert
            Assert.Equal(c, result);
        }

        [Fact]
        public void GetOddNumbers_LimitIsGraterThenZero_ReturnOddNumbersUpToLimit()
        {
            // Act
            IEnumerable<int> result = math.GetOddNumbers(5);

            // Assert
            // Assert.Equal(3, result.Count());
            Assert.Equivalent(new[] { 1, 3, 5 }, result);
        }
    }
}
