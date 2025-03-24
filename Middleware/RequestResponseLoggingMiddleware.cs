using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the incoming request
        var method = context.Request.Method;
        var path = context.Request.Path;
        _logger.LogInformation($"Incoming Request: Method = {method}, Path = {path}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log the outgoing response
        var statusCode = context.Response.StatusCode;
        _logger.LogInformation($"Outgoing Response: Status Code = {statusCode}");
    }
}
