namespace PocApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnNewsClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NewsPage));
	}

	private async void OnMoviesClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(MoviesPage));
	}
}