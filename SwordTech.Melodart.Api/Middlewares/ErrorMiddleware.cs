using System.Net;
using Newtonsoft.Json;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Api.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Middleware: {ex.Message}");

            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            var responseData = new ApiResponse();
            responseData.Message = ex.Message;
            responseData.Errors.Add(ex.Message);

            if (ex.GetType() == typeof(BusinessException))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await response.WriteAsync(JsonConvert.SerializeObject(responseData));
        }
    }
}


public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}
