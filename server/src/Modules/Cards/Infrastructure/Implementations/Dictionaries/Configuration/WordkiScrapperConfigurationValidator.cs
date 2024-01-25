using FluentValidation;

namespace Cards.Infrastructure.Implementations.Dictionaries.Configuration;

internal class WordkiScrapperConfigurationValidator : AbstractValidator<WordkiScrapperConfiguration>
{
    public WordkiScrapperConfigurationValidator()
    {
        RuleFor(x => x.Host)
            .NotEmpty();
    }
}