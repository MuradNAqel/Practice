namespace TestApp.Chapters.Dependency_Injection;

public static class DIClass
{
    public static void RunDIExample()
    {
        //Inject Implementation of EmailNotification INotification 
        INotification emailNotification = new EmailNotification();
        var emailService = new NotificationService(emailNotification);
        emailService.Send("Hi");

        //Inject Implementation of SMSNotification INotification 
        INotification smsNotification = new SMSNotification();
        var smsService = new NotificationService(smsNotification);
        smsService.Send("Hi");
    }
}
