namespace TestApp.Chapters.Dependency_Injection;

public class NotificationService
{
    private readonly INotification _notification;

    public NotificationService(INotification notification)
    {
        _notification = notification;
    }

    public void Send(string message)
    {
        _notification.Notify(message);
    }
}
