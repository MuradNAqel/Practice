using static TestApp.Chapters.Middleware.MiddlewareExample;

namespace TestApp.Chapters.Middleware;

public class ProcessingMiddleware : Middleware
{
    public ProcessingMiddleware(RequestDelegate next) : base(next) { }

    protected override Task HandleRequest()
    {
        Console.WriteLine("ProcessingMiddleware: Request is being processed.");
        return Task.CompletedTask;
    }
}
