using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Infrastructure.Implementations.Dictionary.Models;
using Serilog;

namespace Cards.Infrastructure.Implementations.Dictionary;

internal class DictionaryDevApi : IDictionary
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new ()
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