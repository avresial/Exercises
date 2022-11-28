using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class ErrorLoggerTests
    {
        [Fact]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            ErrorLogger errorLogger = new ErrorLogger();

            errorLogger.Log("a");

            Assert.Equal("a", errorLogger.LastError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            ErrorLogger errorLogger = new ErrorLogger();

            Exception ex = Record.Exception(() => errorLogger.Log(error));

            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Log_ValidError_RiseErrorLoggedEvent()
        {
            ErrorLogger errorLogger = new ErrorLogger();

            var id = Guid.Empty;

            errorLogger.ErrorLogged += (sender, args) => { id = args; };

            errorLogger.Log("a");

            Assert.NotEqual(Guid.Empty, id);
        }
    }
}
