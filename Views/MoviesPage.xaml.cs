using Microsoft.Extensions.DependencyInjection;
using PocApp.Services;

namespace PocApp;

public partial class MoviesPage : ContentPage
{
    private readonly MovieService _movieService;

    public MoviesPage()
    {
        InitializeComponent();
        _movieService = MauiProgram.Services.GetRequiredService<MovieService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMoviesAsync();
    }

    private async Task LoadMoviesAsync()
    {
        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;
            StatusLabel.Text = "Loading movies...";

            var movies = await _movieService.GetMoviesAsync();
            MoviesCollectionView.ItemsSource = movies;

            StatusLabel.Text = movies.Any()
                ? $"Loaded {movies.Count} movie(s)."
                : "No movies found.";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Failed to load movies: {ex.Message}";
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }
}