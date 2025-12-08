using System.Security.Authentication;
using EducationContentService.Domain.Exceptions;
using EducationContentService.Domain.Shared;

namespace EducationContentService.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,  Exception e)
    {
        _logger.LogError(e, "Exceptions was thrown in education service");

        (int statusCode, Error error) = e switch
        {
            NotFoundException ex => (StatusCodes.Status404NotFound, ex.Error),
            ValidationException ex => (StatusCodes.Status400BadRequest, ex.Error),
            ConflictException ex => (StatusCodes.Status409Conflict, ex.Error),
            FailureException ex => (StatusCodes.Status500InternalServerError, ex.Error),
            AuthenticationException => (StatusCodes.Status401Unauthorized, Error.Failure("authentication.failed", e.Message)),
            _ => (StatusCodes.Status500InternalServerError, Error.Failure("server.internal", e.Message)),
        };

        Envelope envelope = Envelope.Fail(error);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(envelope);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}