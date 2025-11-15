using System;
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

    /// <summary>
    /// Get a page of news using Sitefinity OData paging ($skip / $top).
    /// </summary>
    public async Task<IReadOnlyList<NewsItem>> GetNewsPageAsync(int skip, int take)
    {
        var url = $"/api/default/newsitems?$orderby=PublicationDate desc&$skip={skip}&$top={take}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<NewsResponse>(stream, _jsonOptions);

        return data?.Value ?? new List<NewsItem>();
    }

    /// <summary>
    /// Get a single news item by Id.
    /// Uses the OData entity endpoint: /api/default/newsitems({id})
    /// </summary>
    public async Task<NewsItem?> GetNewsByIdAsync(Guid id)
    {
        var url = $"/api/default/newsitems({id})";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        await using var stream = await response.Content.ReadAsStreamAsync();
        var item = await JsonSerializer.DeserializeAsync<NewsItem>(stream, _jsonOptions);

        return item;
    }
}