using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void GetOutput_NumberIsDivisibleOnlyBy3_ReturnsFizz()
        {
            // Act
            string result = FizzBuzz.GetOutput(6);

            // Assert
            Assert.Equal("fizz", result.ToLower());
        }

        [Fact]
        public void GetOutput_NumberIsDivisibleOnlyBy5_ReturnsBuzz()
        {
            // Act
            string result = FizzBuzz.GetOutput(10);

            // Assert
            Assert.Equal("buzz", result.ToLower());
        }

        [Fact]
        public void GetOutput_NumberIsDivisibleBy3And5_ReturnsFizzBuzz()
        {
            // Act
            string result = FizzBuzz.GetOutput(30);

            // Assert
            Assert.Equal("fizzbuzz", result.ToLower());
        }

        [Fact]
        public void GetOutput_NumberIsNotDivisibleBy3or5_ReturnsNumber()
        {
            // Act
            string result = FizzBuzz.GetOutput(31);

            // Assert
            Assert.Equal("31", result.ToLower());
        }

    }
}
