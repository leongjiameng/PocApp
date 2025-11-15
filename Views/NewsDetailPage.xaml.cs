using PocApp.Models;

namespace PocApp;

public partial class NewsDetailPage : ContentPage
{
    private readonly NewsItem _newsItem;

    public NewsDetailPage(NewsItem item)
    {
        InitializeComponent();
        _newsItem = item;

        TitleLabel.Text = _newsItem.Title ?? "(No title)";

        if (_newsItem.PublicationDate != default)
        {
            DateLabel.Text = $"Published: {_newsItem.PublicationDate:yyyy-MM-dd}";
        }
        else
        {
            DateLabel.Text = string.Empty;
        }

        var htmlContent = _newsItem.Content ?? _newsItem.Summary ?? string.Empty;

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

        var source = new HtmlWebViewSource
        {
            Html = html
        };

        ContentWebView.Source = source;
    }
}