using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class WeatherInfoPage : ContentPage
{
	private readonly WeatherInfoViewModel _weatherInfoViewModel;
	public List<string> CountryList;
	public WeatherInfoPage(WeatherInfoViewModel weatherInfoViewModel)
	{
		InitializeComponent();
		_weatherInfoViewModel = weatherInfoViewModel;
		BindingContext = _weatherInfoViewModel;

	}


	protected override void OnAppearing()
	{
		_weatherInfoViewModel.Initialize();
	}

	private void Picker_SelectedRegion(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		int selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
			_weatherInfoViewModel.Region = _weatherInfoViewModel.RegionList.FirstOrDefault(c=>c.Name== picker.Items[selectedIndex]);
	}

    private void Picker_SelectedCity(object sender, EventArgs e)
    {
			var picker = (Picker)sender;
		int selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
			_weatherInfoViewModel.City = picker.Items[selectedIndex];
    }


}