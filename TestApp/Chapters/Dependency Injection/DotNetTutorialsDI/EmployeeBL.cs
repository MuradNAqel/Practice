namespace TestApp.Chapters.Dependency_Injection.DotNetTutorialsDI
{
    //Client Class or Dependent Object
    //This is the Class that is going to consume the services provided by the IEmployeeDAL Class
    //  on the IEmployeeDAL Class
    public class EmployeeBL
    {
        public IEmployeeDAL _employeeDAL;
        //Injecting the Dependency Object using Constructor means it is a Loose Coupling
        public EmployeeBL(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }
        public List<Employee> GetAllEmployees()
        {
            return _employeeDAL.SelectAllEmployees();
        }
    }
}
