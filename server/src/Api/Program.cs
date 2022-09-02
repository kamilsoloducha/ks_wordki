using System;
using Api.Configuration;
using Blueprints.Application.Services;
using Blueprints.Infrastrcuture;
using Blueprints.Infrastrcuture.Services;
using Cards.Application;
using Cards.Domain;
using Cards.Infrastructure;
using FluentValidation.AspNetCore;
using Infrastructure.Rabbit;
using Infrastructure.Services;
using Lessons.Application;
using Lessons.Infrastructure;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Users.Application;
using Users.Domain;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.ConfigureMassTransit();
builder.Services.AddIConnectionStringProvider(builder.Configuration);

var configurator = new ServiceCollectionBusConfigurator(builder.Services);
configurator.AddCardsConsumers();
configurator.AddLessonsConsumers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));
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
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddHashIds(builder.Configuration, builder.Environment);

builder.Services.AddCustomAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddMvc().AddFluentValidation();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

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


app.Run();

public partial class Program { }