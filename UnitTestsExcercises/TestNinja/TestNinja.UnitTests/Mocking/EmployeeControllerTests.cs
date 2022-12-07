using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class EmployeeControllerTests
    {
        Mock<IEmployeeStorage> EmployeeStorage;
        EmployeeController employeeController;
        public EmployeeControllerTests()
        {
            EmployeeStorage = new Mock<IEmployeeStorage>();
            employeeController = new EmployeeController(EmployeeStorage.Object);
        }

        [Fact]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            employeeController.DeleteEmployee(1);

            EmployeeStorage.Verify(s => s.RemoveEmployee(1));
        }

        [Fact]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResultObject()
        {
            var result = employeeController.DeleteEmployee(1);

            Assert.IsType<RedirectResult>(result);
        }
    }
}
