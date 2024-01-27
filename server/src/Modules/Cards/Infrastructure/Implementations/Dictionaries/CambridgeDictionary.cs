using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using Serilog;

namespace Cards.Infrastructure.Implementations.Dictionaries;

internal class CambridgeDictionary(IHttpClientFactory httpClientFactory) : IDictionary
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Cambridge");

    public async Task<IEnumerable<Translation>> Translate(string searchingTerm,
        CancellationToken cancellationToken = default)
    {
        var uri = new Uri($"/cambridge/{searchingTerm}", UriKind.Relative);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<IEnumerable<Translation>>(cancellationToken);

        Log.Warning("Request has failed, StatusCode: {StatusCode}, {@Content}",
            response.StatusCode.ToString(),
            await response.Content.ReadAsStringAsync(cancellationToken)
        );
        return null;
    }
}