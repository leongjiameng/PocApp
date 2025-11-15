using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PocApp.Services;

namespace PocApp;

public static class MauiProgram
{
	public static IServiceProvider Services { get; private set; } = default!;

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Typed HttpClients
		builder.Services.AddHttpClient<MovieService>(client =>
		{
			client.BaseAddress = new Uri(ApiConfig.BaseUrl);
			client.Timeout = TimeSpan.FromSeconds(30);
		});

		builder.Services.AddHttpClient<NewsService>(client =>
		{
			client.BaseAddress = new Uri(ApiConfig.BaseUrl);
			client.Timeout = TimeSpan.FromSeconds(30);
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();
		Services = app.Services;

		return app;
	}
}