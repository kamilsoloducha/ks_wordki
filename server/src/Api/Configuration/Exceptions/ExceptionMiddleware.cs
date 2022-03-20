using System;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Blueprints.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug("Exception middleware");
            try
            {
                await _next(context);
            }
            catch (BuissnessRuleFailedException ex)
            {
                _logger.LogError("A buissness rule '{rule}' has been breached", ex.Rule.GetType().Name);
                await HandleBuissnessException(context, ex);
            }
            catch (BuissnessArgumentException ex)
            {
                _logger.LogCritical("Domain object creation failed. Argument {argument} cannot have value {value}", ex.ArgumentName, ex.Value);
                var response = ResponseBase<object>.Create(ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                _logger.LogError("A buissness rule '{rule}' has been breached", e);
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
}