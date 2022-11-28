using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests.Fundamentals
{
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator demeritPointsCalculator;
        
        public DemeritPointsCalculatorTests()
        {
            demeritPointsCalculator = new DemeritPointsCalculator();
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(65, 0)]
        [InlineData(66, 0)]
        [InlineData(64, 0)]
        [InlineData(70, 1)]
        [InlineData(75, 2)]
        [InlineData(300, 47)]
        public void CalculateDemeritPoints_SpeedExceedsTheLimit_ReturnCorrectAmountOfPoints(int speed, int points)
        {
            // Act
            int result = demeritPointsCalculator.CalculateDemeritPoints(speed);

            // Assert
            Assert.Equal(points, result);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(301)]
        [InlineData(int.MaxValue)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ReturnOutOfRangeException(int speed)
        {
            // Act
            Exception ex = Record.Exception(() => demeritPointsCalculator.CalculateDemeritPoints(speed));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(ex);
        }
    }
}
