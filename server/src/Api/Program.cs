using System;
using Api.Configuration;
using Api.Configuration.Exceptions;
using Application.Services;
using Cards.Application;
using Cards.Domain;
using Cards.Infrastructure;
using FluentValidation.AspNetCore;
using Infrastructure.Rabbit;
using Infrastructure.Services;
using Infrastructure.Services.ConnectionStringProvider;
using Infrastructure.Services.HashIds;
using Lessons.Application;
using Lessons.Infrastructure;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Users.Application;
using Users.Domain;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();
builder.Services.ConfigureMassTransit();
builder.Services.AddIConnectionStringProvider(builder.Configuration);

var configurator = new ServiceCollectionBusConfigurator(builder.Services);
configurator.AddCardsConsumers();
configurator.AddLessonsConsumers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();
builder.Services
    .AddUsersInfrastructureModule(builder.Configuration)
    .AddUsersApplicationModule()
    .AddUsersDomainModule()
    .AddCardsInfrastructureModule(builder.Configuration)
    .AddCardsApplicationModule()
    .AddCardDomainModule()
    .AddLessonsInfrastructureModule(builder.Configuration)
    .AddLessonsApplicationModule()
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddHashIds(builder.Configuration, builder.Environment);

builder.Services.AddCustomAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString().Replace("+","."));
});
builder.Services.AddMvc().AddFluentValidation();
builder.Services.AddCustomLogging();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
app.UseCustomCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

namespace Api
{
    public partial class Program { }
}