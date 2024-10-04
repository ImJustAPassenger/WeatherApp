
using WeatherApp.Pages;

namespace WeatherApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	private readonly static Type[] _routablePageTypes = [
		typeof(MapPage),
		typeof(WeatherInfoPage),
	];

	private void RegisterRoutes()
	{
		foreach (var pageType in _routablePageTypes)
		{
			Routing.RegisterRoute(pageType.Name, pageType);
		}
	}
}
