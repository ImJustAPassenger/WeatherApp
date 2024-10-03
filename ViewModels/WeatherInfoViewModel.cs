using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;
namespace WeatherApp.ViewModels
{
    public partial class WeatherInfoViewModel : ObservableObject
    {
        
    private readonly WeatherApiService _weatherApiService;
    public WeatherInfoViewModel(WeatherApiService weatherApiService)
    {
        _weatherApiService = weatherApiService;
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
        [RelayCommand]
        private async Task FetchWeatherInformation()
        {
            Console.WriteLine("inside the command");
            var weatherApiResponse =  await _weatherApiService.GetWeatherInformation(Latitude,Longitude);
            if(weatherDescription!=null)
            {
                WeatherIcon= weatherApiResponse.current.weather_icons[0];
                Temperature= $"{weatherApiResponse.current.temperature}°C";
                Location =$"{weatherApiResponse.location.name},{weatherApiResponse.location.region  },{weatherApiResponse.location.country}";
                WeatherDescription=weatherApiResponse.current.weather_descriptions[0];
                Humidity =$"{weatherApiResponse.current.humidity}";
                CloudCoverLevel= $"{weatherApiResponse.current.cloudcover}%";
                IsDay = weatherApiResponse.current.is_day.ToUpper();
            }
         }

    }
}