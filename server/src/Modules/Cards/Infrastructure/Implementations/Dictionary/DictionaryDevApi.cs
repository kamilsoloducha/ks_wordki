using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Cards.Application.Services;
using Cards.Domain.ValueObjects;
using Cards.Infrastructure.Implementations.Dictionary.Models;
using HtmlAgilityPack;
using Serilog;

namespace Cards.Infrastructure.Implementations.Dictionary;

internal class DictionaryDevApi : IDictionary
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;

    public DictionaryDevApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DictionaryResponse> Translate(DictionaryRequest request, CancellationToken cancellationToken)
    {
        var searchPhrase = request.Word.Replace(" ", "%20");
        var uri = $"/api/v2/entries/en/{searchPhrase}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpResponseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken));
        }

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<DictionaryDevApiResponse[]>(
            JsonSerializerOptions,
            cancellationToken);

        var meanings = response.SelectMany(x => x.Meanings);
        var translations = meanings.SelectMany(x => x.Definitions)
            .Select(x => new Translation(x.Definition, new[] { x.Example }));

        return new DictionaryResponse(translations);
    }
}

internal class CambridgeDictionary : IDictionary
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public CambridgeDictionary(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<DictionaryResponse> Translate(DictionaryRequest request, CancellationToken cancellationToken)
    {
        var searchPhrase = request.Word.Replace(" ", "-");
        var uri = $"/dictionary/english/{searchPhrase}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);

        var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        _logger.Information(response);

        return null;
    }
}

internal class MacmillanDictionary : IDictionary
{
    private readonly HttpClient _httpClient;

    public MacmillanDictionary(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DictionaryResponse> Translate(DictionaryRequest request,
        CancellationToken cancellationToken = default)
    {
        var searchPhrase = request.Word.Replace(" ", "-");
        var uri = $"/dictionary/british/{searchPhrase}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);

        var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        var document = new HtmlDocument();
        document.LoadHtml(response);

        var mainNode = document.DocumentNode.SelectNodes("//*[@id='entryContent']/div").FirstOrDefault();
        if (mainNode is null)
            return null;

        var question = GetQuestion(mainNode);

        var translationNodes = mainNode.SelectNodes("//li");

        var translations = GetTranslations(translationNodes);
        
        return new DictionaryResponse(translations);
    }

    private static string GetQuestion(HtmlNode mainNode)
    {
        var baseNode = mainNode.SelectSingleNode("//span[contains(@class, 'BASE')]");
        return baseNode is null ? string.Empty : baseNode.InnerText;
    }

    private static IEnumerable<Translation> GetTranslations(IEnumerable<HtmlNode> translationsNodes)
    {
        foreach (var translationNode in translationsNodes)
        {
            var definition = translationNode.Descendants("span")
                .FirstOrDefault(y => y.Attributes["class"].Value == "DEFINITION");
            
            if(definition is null) continue;
            
            var examplesNodes = translationNode.Descendants("p").Where(x => x.Attributes["class"].Value == "EXAMPLE");
            var examples = examplesNodes.Select(x => x.InnerText.Trim()).Where(x => !string.IsNullOrEmpty(x));
            
            yield return new Translation(definition.InnerText, examples);
        }
    }
}