using FluentValidation;

namespace Infrastructure.Services.HashIds;

internal class HashIdsValidator : AbstractValidator<HashIdsConfiguration>
{
    public HashIdsValidator()
    {
        RuleFor(x => x.MinLength).GreaterThan(0);
        RuleFor(x => x.Salt).NotEmpty();
    }
}