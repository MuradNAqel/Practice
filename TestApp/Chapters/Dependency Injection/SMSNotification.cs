namespace TestApp.Chapters.Dependency_Injection;

public class SMSNotification : INotification
{
    public void Notify(string msg)
    {
        Console.WriteLine("Sent via SMS: " + msg);
    }
}
