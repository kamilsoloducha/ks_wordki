using System;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.HashIds;

internal class HashIdsValidator : AbstractValidator<HashIdsConfiguration>
{
    public HashIdsValidator()
    {
        RuleFor(x => x.MinLength).GreaterThan(0);
        RuleFor(x => x.Salt).NotEmpty();
    }
}

public class FluentValidateOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    public FluentValidateOptions(string name) => Name = name;

    public string Name { get; }

    public ValidateOptionsResult Validate(string name, TOptions options)
    {
        if (Name != null && Name != name)
            return ValidateOptionsResult.Skip;
        ArgumentNullException.ThrowIfNull(options);

        return ValidateOptionsResult.Success;
    }
}