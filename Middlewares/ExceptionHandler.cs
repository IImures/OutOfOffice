using System.Text.Json;
using OutOfOffice.Exceptions;

namespace OutOfOffice.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await ReturnResponse(context, ex.StatusCode, ex.Message);
        }
        catch (UserExitsException ex)
        {
            await ReturnResponse(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            await ReturnResponse(context, 500, ex.Message);
        }
    }
    
    private async Task ReturnResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        string jsonResponse = JsonSerializer.Serialize(new
        {
            Message = message,
            Time = DateTime.Now
        });
        
        
        await context.Response.WriteAsync(jsonResponse);
    }
}