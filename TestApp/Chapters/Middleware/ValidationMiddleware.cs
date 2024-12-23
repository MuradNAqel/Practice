using static TestApp.Chapters.Middleware.MiddlewareExample;

namespace TestApp.Chapters.Middleware;

public class ValidationMiddleware : Middleware
{
    public ValidationMiddleware(RequestDelegate next) : base(next) { }

    protected override Task HandleRequest()
    {
        Console.WriteLine("ValidationMiddleware: Request is being validated.");
        return Task.CompletedTask;
    }
}
