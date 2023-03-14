using FluentValidation;

namespace Infrastructure.Authentication
{
    internal class JwtConfigurationValidator : AbstractValidator<JwtConfiguration>
    {
        public JwtConfigurationValidator()
        {
            RuleFor(x => x.Secret).NotEmpty();
        }
    }
}