using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Commands;

namespace Users.Application
{
    public static class Module
    {
        public static IServiceCollection AddUsersApplicationModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Module).Assembly);

            services.AddTransient<IValidator<RegisterUser.Command>, RegisterUser.CommandValidator>();
            services.AddTransient<IValidator<LoginUser.Command>, LoginUser.CommandValidator>();
            services.AddTransient<IValidator<DeleteUser.Command>, DeleteUser.CommandValidator>();
            services.AddTransient<IValidator<ConfirmEmail.Command>, ConfirmEmail.CommandValidator>();

            services.AddTransient<IValidator<UserDetails.Query>, UserDetails.QueryValidator>();
            return services;
        }
    }
}