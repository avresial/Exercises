using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class HtmlFormatterTests
    {
        [Fact]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            // Arrange
            HtmlFormatter formatter = new HtmlFormatter();

            // Act
            var result = formatter.FormatAsBold("abc");

            // Assert
            Assert.StartsWith("<strong>", result, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("abc", result, StringComparison.OrdinalIgnoreCase);
            Assert.EndsWith("</strong>", result, StringComparison.OrdinalIgnoreCase);
        }
    }
}
