using System.Text;

namespace SwordTech.Melodart.Api.Middlewares;


public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        context.Request.EnableBuffering();
        var requestText = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
        context.Request.Body.Position = 0;

        _logger.LogInformation($"Logging middleware request: {requestText}");

        var tempStream = new MemoryStream();
        context.Response.Body = tempStream;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        await context.Response.Body.CopyToAsync(originalBodyStream);

        _logger.LogInformation($"Logging middleware response: {responseText}");
    }
}


public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLogginMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LoggingMiddleware>();
    }
}
