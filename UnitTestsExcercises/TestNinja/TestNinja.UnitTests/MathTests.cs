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

        [Fact]
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

    }
}
