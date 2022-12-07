namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        Employee FindEmployee(int id);
        void RemoveEmployee(Employee employee);
    }
}