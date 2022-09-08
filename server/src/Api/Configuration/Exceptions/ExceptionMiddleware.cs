using System;
using System.Threading.Tasks;
using Application.Requests;
using Domain;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Api.Configuration.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BuissnessRuleFailedException ex)
        {
            _logger.Error(ex, "A buissness rule '{rule}' has been breached", ex.Rule.GetType().Name);
            await HandleBuissnessException(context, ex);
        }
        catch (BuissnessArgumentException ex)
        {
            _logger.Fatal(ex, "Domain object creation failed. Argument {argument} cannot have value {value}", ex.ArgumentName, ex.Value);
            var response = ResponseBase<object>.Create(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception e)
        {
            _logger.Error(e, "");
            throw;
        }
    }

    private async Task HandleBuissnessException(HttpContext context, BuissnessRuleFailedException exception)
    {
        var response = ResponseBase<object>.Create(exception.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(response);
    }

}