using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class MathTests
    {
        [Fact]
        public void Add_AddsTwoPositiveNumbers_ReturnsTrueValue()
        {
            // Arrange
            Math math = new Math();

            // Act
            int result = math.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnFirstArgument()
        {
            // Arrange
            Math math = new Math();

            // Act
            int result = math.Max(1, 2);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {
            // Arrange
            Math math = new Math();

            // Act
            int result = math.Max(2, 1);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Max_ArgumentsAreEqual_ReturnSecondArgument()
        {
            // Arrange
            Math math = new Math();

            // Act
            int result = math.Max(1, 1);

            // Assert
            Assert.Equal(1, result);
        }

    }
}
