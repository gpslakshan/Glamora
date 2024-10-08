using System.Net;
using System.Text.Json;
using API.Helpers;

namespace API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly IHostEnvironment _environment;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(IHostEnvironment environment, RequestDelegate next)
    {
        _environment = environment;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private Task HandleException(Exception ex, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _environment.IsDevelopment()
            ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal server error");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        return context.Response.WriteAsync(json);
    }
}
