using static TestApp.Chapters.Middleware.MiddlewareExample;

namespace TestApp.Chapters.Middleware;

// Step 4: Create Middleware Pipeline Builder
public class MiddlewarePipeline
{
    private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new();

    public MiddlewarePipeline Use(Func<RequestDelegate, RequestDelegate> middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public RequestDelegate Build()
    {
        RequestDelegate pipeline = () => Task.CompletedTask;

        for (int i = _middlewares.Count - 1; i >= 0; i--)
        {
            pipeline = _middlewares[i](pipeline);
        }

        return pipeline;
    }
}
