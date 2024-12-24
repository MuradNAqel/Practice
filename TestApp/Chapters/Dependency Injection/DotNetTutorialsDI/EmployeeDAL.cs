namespace TestApp.Chapters.Dependency_Injection.DotNetTutorialsDI
{
    //This is the class that is responsible for Interacting with the Database
    //This class is going to be used by the EmpoloyeeBL class
    //That means it is going to be the Dependency Object
    public class EmployeeDAL : IEmployeeDAL
    {
        public List<Employee> SelectAllEmployees()
        {
            List<Employee> ListEmployees = new List<Employee>
            {
                //Get the Employees from the Database
                //for now we are hard coded the employees
                new Employee() { ID = 1, Name = "Ahmed", Department = "IT" },
                new Employee() { ID = 2, Name = "Ostaz Waleed", Department = "HR" },
                new Employee() { ID = 3, Name = "Joe", Department = "Accounting" }
            };
            return ListEmployees;
        }
    }
}
