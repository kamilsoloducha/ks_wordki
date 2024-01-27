using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using Cards.Infrastructure.Implementations.Dictionaries.Configuration;
using E2e.Tests;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using WireMock.Server;
using WireMock.Settings;

namespace Cards.E2e.Tests.Dictionaries;

[TestFixture]
public class DikiDictionariesTest : TestBase
{
    [Test]
    public async Task WhenMakeRequestToDiki_ShouldReturnTranslation()
    {
        // arrange
        var searchingTerm = "test";
        IEnumerable<Translation> wireMockResponse =
            [new Translation { Definition = "Definition", Examples = ["Example1", "Example2"] }];
        var options = AppFactory.Services.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
        using var wireMock = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = [options.Value.Host]
        });

        wireMock
            .Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/diki/{searchingTerm}")
                .UsingGet())
            .RespondWith(WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(wireMockResponse));

        Request = new HttpRequestMessage(HttpMethod.Get, $"/dictionary/diki/{searchingTerm}");

        // act

        await SendRequest();

        // assert

        Response.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<Translation>>();

        response.Should().BeEquivalentTo(wireMockResponse);
    }
    
    [Test]
    public async Task WhenDikiReturnEmpty_ShouldReturnEmpty()
    {
        // arrange
        var searchingTerm = "test";
        IEnumerable<Translation> wireMockResponse = [];
        var options = AppFactory.Services.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
        using var wireMock = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = [options.Value.Host]
        });

        wireMock
            .Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/diki/{searchingTerm}")
                .UsingGet())
            .RespondWith(WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(wireMockResponse));

        Request = new HttpRequestMessage(HttpMethod.Get, $"/dictionary/diki/{searchingTerm}");

        // act

        await SendRequest();

        // assert

        Response.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<Translation>>();

        response.Should().BeEquivalentTo(wireMockResponse);
    }
    
    [Test]
    public async Task WhenDikiReturnMultipleDefinitions_ShouldReturnMultipleDefinitions()
    {
        // arrange
        var searchingTerm = "test";
        IEnumerable<Translation> wireMockResponse = [
            new Translation { Definition = "Definition1", Examples = ["Example1", "Example2"] },
            new Translation { Definition = "Definition2", Examples = ["Example3", "Example4"] },
            new Translation { Definition = "Definition3", Examples = ["Example5", "Example6"] },
        ];
        var options = AppFactory.Services.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
        using var wireMock = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = [options.Value.Host]
        });

        wireMock
            .Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/diki/{searchingTerm}")
                .UsingGet())
            .RespondWith(WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(wireMockResponse));

        Request = new HttpRequestMessage(HttpMethod.Get, $"/dictionary/diki/{searchingTerm}");

        // act

        await SendRequest();

        // assert

        Response.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<Translation>>();

        response.Should().BeEquivalentTo(wireMockResponse);
    }
    
    [Test]
    public async Task WhenMakeRequestToDikiAndError_ShouldReturnInternalServerError()
    {
        // arrange
        var searchingTerm = "test";
        var options = AppFactory.Services.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
        using var wireMock = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = [options.Value.Host]
        });

        wireMock
            .Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/diki/{searchingTerm}")
                .UsingGet())
            .RespondWith(WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(404));

        Request = new HttpRequestMessage(HttpMethod.Get, $"/dictionary/diki/{searchingTerm}");

        // act

        await SendRequest();

        // assert

        Response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}