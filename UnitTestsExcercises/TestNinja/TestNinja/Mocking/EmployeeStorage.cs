namespace TestNinja.Mocking
{
    public class EmployeeStorage : IEmployeeStorage
    {

        private EmployeeContext _db;

        public EmployeeStorage(EmployeeContext employeeContext = null)
        {
            _db = employeeContext ?? new EmployeeContext();
        }

        public Employee FindEmployee(int id)
        {
            return _db.Employees.Find(id);
        }

        public void RemoveEmployee(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }

    }
}
