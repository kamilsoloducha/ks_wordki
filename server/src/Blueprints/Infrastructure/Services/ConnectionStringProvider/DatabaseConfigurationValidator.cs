using FluentValidation;

namespace Infrastructure.Services.ConnectionStringProvider
{
    internal class DatabaseConfigurationValidator : AbstractValidator<DatabaseConfiguration>
    {
        public DatabaseConfigurationValidator()
        {
            RuleFor(x => x.Host).NotEmpty();
            RuleFor(x => x.User).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Database).NotEmpty();
            RuleFor(x => x.Port).GreaterThan(0);
        }
    }
}