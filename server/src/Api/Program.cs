using System;
using System.Net;
using Api.Configuration;
using Api.Configuration.Exceptions;
using Api.Model;
using Application.Services;
using Cards.Application;
using Cards.Infrastructure;
using Infrastructure.Rabbit;
using Infrastructure.Services;
using Infrastructure.Services.ConnectionStringProvider;
using Infrastructure.Services.HashIds;
using Lessons.Application;
using Lessons.Infrastructure;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomLogging();
builder.AddCustomConfiguration();
builder.Services.ConfigureMassTransit();
builder.Services.AddConnectionStringProvider(builder.Configuration);

var configurator = new ServiceCollectionBusConfigurator(builder.Services);
configurator.AddCardsConsumers();
configurator.AddLessonsConsumers();

builder.Services.AddModel();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();
builder.Services
    .AddUsersInfrastructureModule(builder.Configuration)
    .AddCardsInfrastructureModule(builder.Configuration)
    .AddLessonsInfrastructureModule(builder.Configuration);
builder.Services.AddHashIds(builder.Configuration, builder.Environment);
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomFluentValidation();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddMvcCore().AddCustomFluentValidationResponse();
builder.Services.AddHealthChecks()
    .AddCheck<PostgresHealthcheck>(nameof(PostgresHealthcheck));
builder.Services.AddHttpsRedirection(o =>
{
    o.HttpsPort = 5001;
    o.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
app.UseCustomCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

// app.CreateCardsScheme()
//     .CreateUserScheme()
//     .CreateLessonScheme();

app.Run();

namespace Api
{
    public partial class Program;
}