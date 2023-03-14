using Api.Model.Requests;
using FluentValidation;

namespace Api.Model.Validators
{
    public sealed class AddCardValidator : AbstractValidator<AddCard>
    {
        public AddCardValidator()
        {
            RuleFor(x => x.Back).NotNull();
            RuleFor(x => x.Front).NotNull();

            RuleFor(x => x.Front.Value).NotEmpty();
            RuleFor(x => x.Back.Value).NotEmpty();
        }
    }
}