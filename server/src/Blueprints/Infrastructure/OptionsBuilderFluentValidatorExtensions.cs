using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class OptionsBuilderFluentValidatorExtensions
{
    public static IServiceCollection BindConfiguration<TOptions>(this IServiceCollection services,
        IConfiguration configuration) where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(typeof(TOptions).Name))
            .ValidateFluently()
            .ValidateOnStart();
        return services;
    }

    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            s => new FluentValidationOptions<TOptions>(optionsBuilder.Name,
                s.GetRequiredService<IValidator<TOptions>>()
            )
        );
        return optionsBuilder;
    }
}