using FluentValidation;

namespace Infrastructure.Services.HashIds;

internal class HashIdConfigurationValidator : AbstractValidator<HashIdsConfiguration>
{
    public HashIdConfigurationValidator()
    {
        RuleFor(x => x.Salt).NotEmpty();
        RuleFor(x => x.MinLength).GreaterThan(0);
    }
}