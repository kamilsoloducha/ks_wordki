using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Model
{
    public static class Module
    {
        public static IServiceCollection AddModel(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(Module).Assembly);
            return services;
        }
    }
}