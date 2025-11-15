namespace PocApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MoviesPage), typeof(MoviesPage));
		Routing.RegisterRoute(nameof(NewsPage), typeof(NewsPage));
		// NewsDetailPage is pushed via constructor; route not required.
	}
}