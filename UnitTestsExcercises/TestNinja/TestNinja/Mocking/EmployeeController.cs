using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        IEmployeeStorage employeeStorage;
        public EmployeeController(IEmployeeStorage employeeStorage)
        {
            this.employeeStorage = employeeStorage ?? new EmployeeStorage() ;
        }

        public ActionResult DeleteEmployee(int id)
        {
            employeeStorage.RemoveEmployee(id);

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}