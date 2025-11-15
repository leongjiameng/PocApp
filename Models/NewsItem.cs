using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PocApp.Models;

public sealed class NewsResponse
{
    [JsonPropertyName("@odata.context")]
    public string? ODataContext { get; set; }

    [JsonPropertyName("value")]
    public List<NewsItem> Value { get; set; } = new();
}

public sealed class NewsItem
{
    public Guid Id { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IncludeInSitemap { get; set; }
    public string? SystemSourceKey { get; set; }
    public string? UrlName { get; set; }
    public string? ItemDefaultUrl { get; set; }
    public string? MetaDescription { get; set; }
    public string? OpenGraphTitle { get; set; }
    public string? MetaTitle { get; set; }
    public string? OpenGraphDescription { get; set; }
    public bool AllowComments { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; } // HTML
    public string? Author { get; set; }
    public string? SourceName { get; set; }
    public string? SourceSite { get; set; }
    public string? Provider { get; set; }
}