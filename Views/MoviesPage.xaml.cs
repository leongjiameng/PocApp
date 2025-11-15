using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using PocApp.Models;
using PocApp.Services;

namespace PocApp;

public partial class MoviesPage : ContentPage
{
    private readonly MovieService _movieService;

    private readonly ObservableCollection<Movie> _movies = new();

    private const int PageSize = 10;
    private int _skip = 0;
    private bool _isLoading = false;
    private bool _hasMore = true;

    public MoviesPage()
    {
        InitializeComponent();
        _movieService = MauiProgram.Services.GetRequiredService<MovieService>();

        MoviesCollectionView.ItemsSource = _movies;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_movies.Count == 0)
        {
            await LoadFirstPageAsync();
        }
    }

    private async Task LoadFirstPageAsync()
    {
        _skip = 0;
        _hasMore = true;
        _movies.Clear();
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

            StatusLabel.Text = _movies.Count == 0
                ? "Loading movies..."
                : "Loading more movies...";

            var page = await _movieService.GetMoviesPageAsync(_skip, PageSize);

            foreach (var m in page)
            {
                _movies.Add(m);
            }

            _skip += page.Count;

            if (page.Count < PageSize)
            {
                // Last page – no more data
                _hasMore = false;
            }

            StatusLabel.Text = _movies.Count > 0
                ? $"Loaded {_movies.Count} movie(s)."
                : "No movies found.";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Failed to load movies: {ex.Message}";
            _hasMore = false; // avoid hammering the API on every scroll if it keeps failing
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            _isLoading = false;
        }
    }

    // Pull to refresh
    private async void OnRefreshRequested(object sender, EventArgs e)
    {
        try
        {
            await LoadFirstPageAsync();
        }
        finally
        {
            MoviesRefreshView.IsRefreshing = false;
        }
    }

    // Infinite scroll trigger
    private async void OnRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        await LoadNextPageAsync();
    }
}