using System.Net;
using System.Text.Json;
using WebApplication1.Exceptions;

namespace WebApplication1.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception.GetType() == typeof(NotFoundException))
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var response = new
            {
               error = new
               {
                   message = exception.Message
               }
            };
            
            var jsonResponse = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(jsonResponse);
        }
        
        else if (exception.GetType() == typeof(InvalidTokenException) || exception.GetType() == typeof(InvalidPasswordException))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var response = new
            {
               error = new
               {
                   message = exception.Message
               }
            };
            
            var jsonResponse = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(jsonResponse);
        }
            
    }
}