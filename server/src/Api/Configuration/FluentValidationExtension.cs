using System.Collections.Generic;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    internal static class FluentValidationExtension
    {
        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true, lifetime: ServiceLifetime.Singleton);
            services.AddValidatorsFromAssembly(typeof(Infrastructure.FluentValidationOptions<>).Assembly, includeInternalTypes: true, lifetime: ServiceLifetime.Singleton);
            services.AddFluentValidationAutoValidation();
            return services;
        }

        public static IMvcCoreBuilder AddCustomFluentValidationResponse(this IMvcCoreBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(x =>
            {
                x.InvalidModelStateResponseFactory = context =>
                {
                    var errors = GetErrors(context.ModelState.Values);
                    return new BadRequestObjectResult(errors);
                };
            });
            return builder;
        }
    
        private static IEnumerable<string> GetErrors(ModelStateDictionary.ValueEnumerable values)
        {
            foreach (var value in values)
            {
                var enumerator = value.Errors.GetEnumerator();
                while (enumerator.MoveNext() && enumerator.Current is not null)
                {
                    yield return enumerator.Current.ErrorMessage;
                }
                enumerator.Dispose();
            }
        }
    }
}