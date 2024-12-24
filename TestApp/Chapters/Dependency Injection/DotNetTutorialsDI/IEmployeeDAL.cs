namespace TestApp.Chapters.Dependency_Injection.DotNetTutorialsDI
{
    //Service Class or Dependency Object
    //Dependency Object should be Interface-Based
    public interface IEmployeeDAL
    {
        List<Employee> SelectAllEmployees();
    }
}
