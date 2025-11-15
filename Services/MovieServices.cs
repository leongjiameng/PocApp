using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PocApp.Models;

namespace PocApp.Services;

public sealed class MovieService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public MovieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Get a page of movies using Sitefinity OData paging ($skip / $top).
    /// </summary>
    public async Task<IReadOnlyList<Movie>> GetMoviesPageAsync(int skip, int take)
    {
        // IMPORTANT: always order when paging
        var url = $"/api/default/movies?$orderby=PublicationDate desc&$skip={skip}&$top={take}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<MoviesResponse>(stream, _jsonOptions);

        return data?.Value ?? new List<Movie>();
    }
}