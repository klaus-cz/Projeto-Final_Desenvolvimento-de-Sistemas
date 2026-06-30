using System.Text.Json;
using ChecklistProductivity.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistProductivity.Api.Middleware;

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
        catch (AppException ex)
        {
            await WriteProblemAsync(context, ex.StatusCode, ex.ErrorCode, ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteProblemAsync(context, 400, "validation_error", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado.");
            await WriteProblemAsync(context, 500, "internal_server_error", "Ocorreu um erro inesperado.");
        }
    }

    private static async Task WriteProblemAsync(HttpContext context, int statusCode, string errorCode, string detail)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = errorCode,
            Detail = detail,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}
