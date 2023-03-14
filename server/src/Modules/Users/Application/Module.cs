using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain;

namespace Users.Application
{
    public static class Module
    {
        public static IServiceCollection AddUsersApplicationModule(this IServiceCollection services)
        {
            services.AddUsersDomainModule();
            services.AddMediatR(typeof(Module).Assembly);

            return services;
        }
    }
}