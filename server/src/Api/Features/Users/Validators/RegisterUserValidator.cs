using FluentValidation;
using Users.Application.Commands;

namespace Api.Features.Users.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser.Command>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}