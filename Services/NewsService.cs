using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PocApp.Models;

namespace PocApp.Services;

public sealed class NewsService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public NewsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<NewsItem>> GetNewsAsync()
    {
        var response = await _httpClient.GetAsync("/api/default/newsitems");
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<NewsResponse>(stream, _jsonOptions);

        return data?.Value ?? System.Array.Empty<NewsItem>();
    }
}