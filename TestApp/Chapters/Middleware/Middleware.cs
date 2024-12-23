using static TestApp.Chapters.Middleware.MiddlewareExample;

namespace TestApp.Chapters.Middleware;

// Step 2: Define Middleware Base Class
public abstract class Middleware
{
    private readonly RequestDelegate _next;

    protected Middleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke()
    {
        await HandleRequest();
        if (_next != null)
        {
            await _next();
        }
    }

    protected abstract Task HandleRequest();
}
