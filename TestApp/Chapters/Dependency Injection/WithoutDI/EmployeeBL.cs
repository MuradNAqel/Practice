namespace TestApp.Chapters.Dependency_Injection.WithoutDI
{
    public class EmployeeBL
    {
        public EmployeeDAL employeeDAL;
        public List<Employee> GetAllEmployees()
        {
            //Creating an Instance of Dependency Class means it is a Tight Coupling
            employeeDAL = new EmployeeDAL();
            return employeeDAL.SelectAllEmployees();
        }
    }
}
