using System.Net;
using System.Text.Json;
using FluentValidation;

namespace PromptAPI.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        object response;
        
        if (exception is ValidationException validationException)
        {
            response = new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                message = "Validation failed",
                errors = validationException.Errors.Select(e => new
                {
                    property = e.PropertyName,
                    message = e.ErrorMessage
                })
            };
        }
        else if (exception is KeyNotFoundException keyNotFoundException)
        {
            response = new
            {
                statusCode = (int)HttpStatusCode.NotFound,
                message = keyNotFoundException.Message,
                errors = Array.Empty<object>()
            };
        }
        else
        {
            response = new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                message = "An internal server error occurred",
                errors = Array.Empty<object>()
            };
        }

        var statusCode = exception switch
        {
            ValidationException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };
        
        context.Response.StatusCode = statusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(jsonResponse);
    }
}
