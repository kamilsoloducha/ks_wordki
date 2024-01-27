using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using Serilog;

namespace Cards.Infrastructure.Implementations.Dictionaries;

internal class DikiDictionary(IHttpClientFactory httpClientFactory) : IDictionary
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Diki");

    public async Task<IEnumerable<Translation>> Translate(string searchingTerm,
        CancellationToken cancellationToken = default)
    {
        var uri = new Uri($"/diki/{searchingTerm}", UriKind.Relative);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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