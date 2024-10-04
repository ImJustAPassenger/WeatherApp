using Microsoft.Extensions.Logging;
using WeatherApp.Pages;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Refit;
using System.Net.Security;
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
			})
			.UseMauiMaps();
	
#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddTransient<WeatherInfoViewModel>()
						.AddSingleton<WeatherInfoPage>()
						.AddSingleton<MapPage>();


		ConfigureRefit(builder.Services);
		return builder.Build();
	}

	private static void ConfigureRefit(IServiceCollection services)
	{

		services.AddRefitClient<IWeatherApi>(GetRefitSettings)
		.ConfigureHttpClient(SetHttpClient);

		static void SetHttpClient(HttpClient httpClient)
		{
			var baseUrl = "http://api.weatherstack.com";
			httpClient.BaseAddress = new Uri(baseUrl);

			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

		};


		static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
		{

			var refitSettings = new RefitSettings
			{
				HttpMessageHandlerFactory = () =>
				{
					return new HttpClientHandler
					{
						ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
					   {
						   return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
					   }
					};
				}
			};
			return refitSettings;

		}
	}

}
