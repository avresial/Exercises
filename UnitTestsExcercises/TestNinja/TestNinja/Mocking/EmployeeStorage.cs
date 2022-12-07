namespace TestNinja.Mocking
{
    public class EmployeeStorage : IEmployeeStorage
    {

        private EmployeeContext _db;

        public EmployeeStorage(EmployeeContext employeeContext = null)
        {
            _db = employeeContext ?? new EmployeeContext();
        }

        public void RemoveEmployee(int id)
        {
            Employee employee = _db.Employees.Find(id);
            if (employee == null) return;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }

    }
}
