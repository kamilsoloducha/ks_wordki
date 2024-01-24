using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using Cards.Infrastructure.Implementations.Dictionaries.Configuration;
using Cards.Infrastructure.Implementations.Dictionaries.Models;
using Microsoft.Extensions.Options;
using Serilog;

namespace Cards.Infrastructure.Implementations.Dictionaries;

internal class ApiDevDictionary : IDictionary
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly string _version;

    public ApiDevDictionary(IHttpClientFactory httpClientFactory,
        IOptions<ApiDictionaryConfiguration> options)
    {
        _httpClient = httpClientFactory.CreateClient("ApiDictionary");
        _version = options.Value.Version;
    }

    public async Task<IEnumerable<Translation>> Translate(string searchingTerm, CancellationToken cancellationToken)
    {
        var searchPhrase = searchingTerm.Replace(" ", "%20");
        var uri = $"/api/{_version}/entries/en/{searchPhrase}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpResponseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request has failed, StatusCode: {StatusCode}, {@Content}",
                httpResponseMessage.StatusCode.ToString(),
                await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken)
            );
            return null;
        }

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<DictionaryDevApiResponse[]>(
            JsonSerializerOptions,
            cancellationToken);

        var meanings = response.SelectMany(x => x.Meanings);
        var translations = meanings.SelectMany(x => x.Definitions)
            .Select(x => new Translation
            {
                Definition = x.Definition,
                Examples = x.Example is null ? ReadOnlyCollection<string>.Empty : new[] { x.Example }
            });

        return translations;
    }
}