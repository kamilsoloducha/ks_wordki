using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.Performance.Tests;

[TestFixture]
public class RegisterUserTests
{
    private static int _counter;
    
    [Test]
    public void UsersRegister()
    {
        const string url = "http://localhost:5000/users/register";

        var registerUserStop = Step.Create("Register User", HttpClientFactory.Create(), async context =>
        {
            try
            {
                var counter = GetCounter();
                var body = new
                {
                    UserName = $"user{counter}",
                    Password = "pass",
                    Email = $"user{counter}@email.com",
                    FirstName = "name",
                    Surname = "surname"
                };
                var request = Http.CreateRequest("POST", url)
                    .WithBody(new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

                return await Http.Send(request, context);
            }
            catch (Exception)
            {
                return Response.Fail();
            }
        });

        var scenario = ScenarioBuilder.CreateScenario("user/register", registerUserStop)
            .WithWarmUpDuration(TimeSpan.FromSeconds(3))
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(20, TimeSpan.FromSeconds(10)));

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }

    private static int GetCounter()
    {
        return Interlocked.Increment(ref _counter);
    }
}