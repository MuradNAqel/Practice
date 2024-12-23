namespace TestApp.Chapters.Middleware;

public class MiddlewareExample
{

    // Step 1: Define a Middleware Delegate
    public delegate Task RequestDelegate();

    // Step 5: Demonstrate in a Console Application
    public async static void RunMiddleware()
    {
        // Create the middleware pipeline
        var pipeline = new MiddlewarePipeline()
            .Use(next => new LoggingMiddleware(next).Invoke)
            .Use(next => new ValidationMiddleware(next).Invoke)
            .Use(next => new ProcessingMiddleware(next).Invoke)
            .Build();

        // Execute the pipeline
        Console.WriteLine("Starting middleware pipeline...");
        await pipeline();
        Console.WriteLine("Pipeline finished.");
    }
}
