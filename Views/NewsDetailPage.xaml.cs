using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PocApp.Models;
using PocApp.Services;

namespace PocApp;

[QueryProperty(nameof(NewsId), "id")]
public partial class NewsDetailPage : ContentPage
{
    private readonly NewsService _newsService;

    public string? NewsId { get; set; }

    public NewsDetailPage()
    {
        InitializeComponent();
        _newsService = MauiProgram.Services.GetRequiredService<NewsService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (string.IsNullOrWhiteSpace(NewsId))
        {
            await DisplayAlertAsync("Error", "No news id provided.", "OK");
            return;
        }

        if (!Guid.TryParse(NewsId, out var id))
        {
            await DisplayAlertAsync("Error", "Invalid news id.", "OK");
            return;
        }

        await LoadNewsDetailAsync(id);
    }

    private async Task LoadNewsDetailAsync(Guid id)
    {
        var item = await _newsService.GetNewsByIdAsync(id);

        if (item == null)
        {
            await DisplayAlertAsync("Error", "News item not found.", "OK");
            return;
        }

        TitleLabel.Text = item.Title ?? "(No title)";

        if (item.PublicationDate != default)
        {
            DateLabel.Text = $"Published: {item.PublicationDate:yyyy-MM-dd}";
        }
        else
        {
            DateLabel.Text = string.Empty;
        }

        var htmlContent = item.Content ?? item.Summary ?? string.Empty;

        var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
            margin: 0;
            padding: 0 8px 16px 8px;
            color: #222;
            line-height: 1.5;
        }}
        h1,h2,h3,h4,h5,h6 {{
            margin-top: 1em;
        }}
        p {{
            margin: 0.5em 0;
        }}
    </style>
</head>
<body>
    {htmlContent}
</body>
</html>";

        ContentWebView.Source = new HtmlWebViewSource
        {
            Html = html
        };
    }
}