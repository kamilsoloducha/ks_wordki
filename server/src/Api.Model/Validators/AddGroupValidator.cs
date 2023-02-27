using Api.Model.Requests;
using FluentValidation;

namespace Api.Model.Validators;

public sealed class AddGroupValidator : AbstractValidator<AddGroup>
{
    public AddGroupValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Front).GreaterThan(0);
        RuleFor(x => x.Back).GreaterThan(0);
    }
}