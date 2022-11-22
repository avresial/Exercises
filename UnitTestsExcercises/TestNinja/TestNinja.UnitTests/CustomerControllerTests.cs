using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class CustomerControllerTests
    {
        CustomerController controlerObject;

        public CustomerControllerTests()
        {
            controlerObject = new CustomerController();
        }

        [Fact]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            // Act
            ActionResult result = controlerObject.GetCustomer(0);

            // Assert
            Assert.IsType<NotFound>(result);
        }

        [Fact]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            // Act
            ActionResult result = controlerObject.GetCustomer(1);

            // Assert
            Assert.IsType<Ok>(result);
        }
    }
}
