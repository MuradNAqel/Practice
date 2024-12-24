//    The main objective of the Dependency Injection Design Pattern is that today you are using the EmployeeDAL class instance,
//    tomorrow you might create a new EmployeeDAL class, let us say EmployeeDAL2 which is pointing to a different database or do some unit testing, 
//    then your EmployeeBL code is going to be unchanged. Simply, create the EmployeeDAL2 instance and pass that instance as an 
//    argument to the EmployeeBL class constructor while creating the instance of EmployeeBL class. 

namespace TestApp.Chapters.Dependency_Injection.DotNetTutorialsDI
{
    public static class RunDI
    {
        public static void Run()
        {
            //Create an Instance of EmployeeBL and Inject the Dependency Object as an Argument to the Constructor
            EmployeeBL employeeBL = new EmployeeBL(new EmployeeDAL());
            List<Employee> ListEmployee = employeeBL.GetAllEmployees();

            foreach (Employee emp in ListEmployee)
            {
                Console.WriteLine($"ID = {emp.ID}, Name = {emp.Name}, Department = {emp.Department}");
            }
            Console.ReadKey();
        }
    }
}
