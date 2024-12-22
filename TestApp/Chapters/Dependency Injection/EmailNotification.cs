namespace TestApp.Chapters.Dependency_Injection;

public class EmailNotification : INotification
{
    public void Notify(string msg)
    {
        Console.WriteLine("Sent via Email: " + msg);
    }
}
