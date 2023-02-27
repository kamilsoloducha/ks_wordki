using Api.Model.Requests;
using FluentValidation;

namespace Api.Model.Validators;

public sealed class UpdateGroupValidator : AbstractValidator<UpdateGroup>
{
    public UpdateGroupValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Front).GreaterThan(0);
        RuleFor(x => x.Back).GreaterThan(0);
    }
}