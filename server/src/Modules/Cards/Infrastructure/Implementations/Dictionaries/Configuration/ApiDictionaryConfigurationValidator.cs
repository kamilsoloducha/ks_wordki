using FluentValidation;

namespace Cards.Infrastructure.Implementations.Dictionaries.Configuration;

internal class ApiDictionaryConfigurationValidator : AbstractValidator<ApiDictionaryConfiguration>
{
    public ApiDictionaryConfigurationValidator()
    {
        RuleFor(x => x.Host)
            .NotEmpty();

        RuleFor(x => x.Version)
            .NotEmpty();
    }
}