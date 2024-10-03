using Microsoft.Extensions.Logging;
using WeatherApp.Pages;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp;

public static class MauiProgram
{
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
		builder.Services.AddHttpClient(WeatherApiService.ClientName, HttpClient => HttpClient.BaseAddress = new Uri("http://api.weatherstack.com/"));

#if DEBUG
		builder.Logging.AddDebug();
#endif
builder.Services.AddSingleton<WeatherInfoViewModel>().AddSingleton<WeatherInfoPage>();

		return builder.Build();
	}
}
