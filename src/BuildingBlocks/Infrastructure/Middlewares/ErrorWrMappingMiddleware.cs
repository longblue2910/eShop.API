using Microsoft.AspNetCore.Http;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Middlewares;

public class ErrorWrMappingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorWrMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var errorMsg = string.Empty;
        try
        {
            await _next.Invoke(context);
        }
        catch (ValidationException ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
