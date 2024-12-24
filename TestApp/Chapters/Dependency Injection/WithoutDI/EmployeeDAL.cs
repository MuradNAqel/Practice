namespace TestApp.Chapters.Dependency_Injection.WithoutDI
{
    public class EmployeeDAL
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
