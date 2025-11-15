using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PocApp.Models;

namespace PocApp.Services;

public sealed class MovieService
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://pro.84d21.cloud.sitefinity.com")
    };

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<IReadOnlyList<Movie>> GetMoviesAsync()
    {
        var response = await _httpClient.GetAsync("/api/default/movies");
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<MoviesResponse>(stream, _jsonOptions);

        return data?.Value ?? Array.Empty<Movie>();
    }
}