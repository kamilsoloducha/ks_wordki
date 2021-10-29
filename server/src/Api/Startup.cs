using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Users.Application;
using Users.Domain;
using Users.Infrastructure;
using Api.Configuration;
using Cards.Infrastructure;
using Cards.Application;
using Infrastructure.Rabbit;
using Lessons.Infrastructure;
using Lessons.Application;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using Blueprints.Application.Services;
using Blueprints.Infrastrcuture.Services;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMassTransit();

            var configurator = new ServiceCollectionBusConfigurator(services);
            configurator.AddCardsConsumers();
            configurator.AddLessonsConsumers();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserDataProvider, UserDataProvider>();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));
            services
                .AddUsersInfrastructureModule(Configuration)
                .AddUsersApplicationModule()
                .AddUsersDomainModule()
                .AddCardsInfrastructureModule(Configuration)
                .AddCardsApplicationModule()
                .AddLessonsInfrastructureModule(Configuration)
                .AddLessonsApplicationModule()
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCustomAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            services.AddMvc().AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app,
         IWebHostEnvironment env,
         IServiceProvider serviceProvider)
        {
            // serviceProvider.CreateUsersDb().Wait();
            // serviceProvider.CreateCardsDb().Wait();
            // serviceProvider.CreateLessonsDb().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }


            app.UseCors("AllowAll");
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<PerformanceMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
