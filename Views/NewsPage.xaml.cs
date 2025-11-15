using Microsoft.Extensions.DependencyInjection;
using PocApp.Models;
using PocApp.Services;

namespace PocApp;

public partial class NewsPage : ContentPage
{
    private readonly NewsService _newsService;

    public NewsPage()
    {
        InitializeComponent();
        _newsService = MauiProgram.Services.GetRequiredService<NewsService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadNewsAsync();
    }

    private async Task LoadNewsAsync()
    {
        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;
            StatusLabel.Text = "Loading news...";

            var items = await _newsService.GetNewsAsync();
            NewsCollectionView.ItemsSource = items;

            StatusLabel.Text = items.Any()
                ? $"Loaded {items.Count} news item(s)."
                : "No news found.";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Failed to load news: {ex.Message}";
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    private async void OnNewsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var selected = e.CurrentSelection.FirstOrDefault() as NewsItem;
            if (selected == null)
                return;

            // allow re-tap later
            NewsCollectionView.SelectedItem = null;

            await Shell.Current.Navigation.PushAsync(new NewsDetailPage(selected));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to open news: {ex.Message}", "OK");
        }
    }
}