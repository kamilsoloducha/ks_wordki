using FluentValidation;
using Users.Application.Commands;

namespace Api.Features.Users.Validators;

internal class LoginUserValidator : AbstractValidator<LoginUser.Command>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}