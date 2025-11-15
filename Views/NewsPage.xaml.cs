using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using PocApp.Models;
using PocApp.Services;

namespace PocApp;

public partial class NewsPage : ContentPage
{
    private readonly NewsService _newsService;

    private readonly ObservableCollection<NewsItem> _news = new();

    private const int PageSize = 10;
    private int _skip = 0;
    private bool _isLoading = false;
    private bool _hasMore = true;

    public NewsPage()
    {
        InitializeComponent();
        _newsService = MauiProgram.Services.GetRequiredService<NewsService>();

        NewsCollectionView.ItemsSource = _news;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_news.Count == 0)
        {
            await LoadFirstPageAsync();
        }
    }

    private async Task LoadFirstPageAsync()
    {
        _skip = 0;
        _hasMore = true;
        _news.Clear();
        await LoadNextPageAsync();
    }

    private async Task LoadNextPageAsync()
    {
        if (_isLoading || !_hasMore)
            return;

        try
        {
            _isLoading = true;
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            StatusLabel.Text = _news.Count == 0
                ? "Loading news..."
                : "Loading more news...";

            var page = await _newsService.GetNewsPageAsync(_skip, PageSize);

            foreach (var item in page)
            {
                _news.Add(item);
            }

            _skip += page.Count;

            if (page.Count < PageSize)
            {
                _hasMore = false;
            }

            StatusLabel.Text = _news.Count > 0
                ? $"Loaded {_news.Count} news item(s)."
                : "No news found.";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Failed to load news: {ex.Message}";
            _hasMore = false;
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            _isLoading = false;
        }
    }

    // Pull-to-refresh
    private async void OnRefreshRequested(object sender, EventArgs e)
    {
        try
        {
            await LoadFirstPageAsync();
        }
        finally
        {
            NewsRefreshView.IsRefreshing = false;
        }
    }

    // Infinite scroll trigger
    private async void OnRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        await LoadNextPageAsync();
    }

    private async void OnNewsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var selected = e.CurrentSelection.FirstOrDefault() as NewsItem;
            if (selected == null)
                return;

            NewsCollectionView.SelectedItem = null;

            var route = $"{nameof(NewsDetailPage)}?id={selected.Id}";
            await Shell.Current.GoToAsync(route);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Unable to open news: {ex.Message}", "OK");
        }
    }
}