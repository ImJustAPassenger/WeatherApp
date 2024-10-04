using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;
namespace WeatherApp.ViewModels
{
    public partial class WeatherInfoViewModel : ObservableObject
    {

        public WeatherInfoViewModel(IWeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }

        [ObservableProperty]
        private string latitude;
        [ObservableProperty]
        private string longitude;

        [ObservableProperty]
        private string weatherIcon;
        [ObservableProperty]
        private string temperature;
        [ObservableProperty]
        private string weatherDescription;
        [ObservableProperty]
        private string location;
        [ObservableProperty]
        private string humidity;
        [ObservableProperty]
        private string cloudCoverLevel;

        [ObservableProperty]
        private string isDay;
        private readonly IWeatherApi _weatherApi;

        private readonly string API_KEY = "fe2d2ac6a671422fbd2136dc1578dc83";

        [RelayCommand]
        private async Task FetchWeatherInformation()
        {

            try
            {
                var response = await _weatherApi.GetCurrentAsync(API_KEY, [Latitude, Longitude]);
                if (response != null)
                {
                    WeatherIcon = response.current.weather_icons[0];
                    Temperature = $"{response.current.temperature}°C";
                    Location = $"{response.location.name},{response.location.region},{response.location.country}";
                    WeatherDescription = response.current.weather_descriptions[0];
                    Humidity = $"{response.current.humidity}";
                    CloudCoverLevel = $"{response.current.cloudcover}%";
                    IsDay = response.current.is_day.ToUpper();
                }
                else
                {
                    await Shell.Current.DisplayAlert("error", "response is null", "ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("error", ex.Message, "ok");
            }
            Console.WriteLine("inside the command");
       
        }

    }
}
