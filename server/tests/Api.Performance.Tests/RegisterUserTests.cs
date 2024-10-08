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
                    UserName = $"1user{counter}",
                    Password = "pass",
                    Email = $"1user{counter}@email.com",
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
    
    // name │ Register User                                           │
    // │      request count │ all = 6889, ok = 6889, RPS = 688.9                      │
    // │            latency │ min = 6.84, mean = 29.02, max = 102.85, StdDev = 10.4   │
    // │ latency percentile │ p50 = 26.86, p75 = 34.24, p95 = 48.86, p99 = 63.65      │
    // │      data transfer │ min = 0.115 KB, mean = 0.115 KB, max = 0.115 KB, all =  │
    // │                    │ 0.8 MB                                                  │

    private static int GetCounter()
    {
        return Interlocked.Increment(ref _counter);
    }
}