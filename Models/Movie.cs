using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PocApp.Models;

public sealed class MoviesResponse
{
    [JsonPropertyName("@odata.context")]
    public string? ODataContext { get; set; }

    [JsonPropertyName("value")]
    public List<Movie> Value { get; set; } = new();
}

public sealed class Movie
{
    public Guid Id { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime PublicationDate { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IncludeInSitemap { get; set; }
    public string? SystemSourceKey { get; set; }
    public string? UrlName { get; set; }
    public string? ItemDefaultUrl { get; set; }
    public DateTime DateOfRelease { get; set; }
    public string? Description { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? Title { get; set; }
    public string? Provider { get; set; }
}