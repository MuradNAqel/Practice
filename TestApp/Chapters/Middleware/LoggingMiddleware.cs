using static TestApp.Chapters.Middleware.MiddlewareExample;

namespace TestApp.Chapters.Middleware;

// Step 3: Implement Specific Middleware Components
public class LoggingMiddleware : Middleware
{
    public LoggingMiddleware(RequestDelegate next) : base(next) { }

    protected override Task HandleRequest()
    {
        Console.WriteLine("LoggingMiddleware: Request is being logged.");
        return Task.CompletedTask;
    }
}
