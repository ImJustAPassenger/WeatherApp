using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class WeatherInfoPage : ContentPage
{
	private readonly WeatherInfoViewModel _weatherInfoViewModel;
	public WeatherInfoPage(WeatherInfoViewModel weatherInfoViewModel)
	{
		InitializeComponent();
		_weatherInfoViewModel = weatherInfoViewModel;
		BindingContext = _weatherInfoViewModel;
	}
}