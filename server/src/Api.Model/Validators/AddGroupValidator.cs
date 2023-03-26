using Api.Model.Requests;
using FluentValidation;

namespace Api.Model.Validators;

public sealed class AddGroupValidator : AbstractValidator<AddGroup>
{
    public AddGroupValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Front).NotEmpty();
        RuleFor(x => x.Back).NotEmpty();
    }
}