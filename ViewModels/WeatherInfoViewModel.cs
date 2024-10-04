using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Models;
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


        [ObservableProperty]
        private List<CountryModel> _regionList;

        [ObservableProperty]
        private CountryModel _region;
         [ObservableProperty]
        private string _city;

        //your_api_key
        private readonly string API_KEY = "fe2d2ac6a671422fbd2136dc1578dc83";

        [RelayCommand]
        private async Task FetchWeatherInformation()
        {

            try
            {
                var query="";
                if(string.IsNullOrWhiteSpace(City))
                {
                    query = Region.Name;
                }
                else
                {
                    query= $"{City},{Region.Name}";
                }
                // var response = await _weatherApi.GetCurrentAsync(API_KEY,$"{Latitude},{Longitude}");
                var response = await _weatherApi.GetCurrentAsync(API_KEY, $"{query}");

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

        }


        public void Initialize()
        {
            var regions = new Dictionary<string, List<string>>
{
    { "Abruzzo", new List<string> { "L'Aquila", "Pescara", "Chieti", "Teramo" } },
    { "Basilicata", new List<string> { "Potenza", "Matera" } },
    { "Calabria", new List<string> { "Catanzaro", "Reggio Calabria", "Cosenza", "Crotone", "Vibo Valentia" } },
    { "Campania", new List<string> { "Napoli", "Salerno", "Caserta", "Benevento", "Avellino" } },
    { "Emilia-Romagna", new List<string> { "Bologna", "Modena", "Parma", "Reggio Emilia", "Rimini", "Ferrara", "Ravenna", "Piacenza" } },
    { "Friuli-Venezia Giulia", new List<string> { "Trieste", "Udine", "Pordenone", "Gorizia" } },
    { "Lazio", new List<string> { "Rome", "Latina", "Frosinone", "Viterbo", "Rieti" } },
    { "Liguria", new List<string> { "Genoa", "La Spezia", "Savona", "Imperia" } },
    { "Lombardia", new List<string> { "Milano", "Bergamo", "Brescia", "Monza", "Como", "Pavia", "Varese", "Mantua", "Lecco", "Lodi", "Cremona", "Sondrio" } },
    { "Marche", new List<string> { "Ancona", "Pesaro", "Urbino", "Macerata", "Fermo", "Ascoli Piceno" } },
    { "Molise", new List<string> { "Campobasso", "Isernia" } },
    { "Piemonte", new List<string> { "Turin", "Novara", "Alessandria", "Asti", "Cuneo", "Biella", "Verbania", "Vercelli" } },
    { "Puglia", new List<string> { "Bari", "Taranto", "Lecce", "Foggia", "Brindisi", "Andria", "Barletta", "Trani" } },
    { "Sardegna", new List<string> { "Cagliari", "Sassari", "Nuoro", "Oristano", "Olbia" } },
    { "Sicilia", new List<string> { "Palermo", "Catania", "Messina", "Syracuse", "Trapani", "Ragusa", "Agrigento", "Caltanissetta", "Enna" } },
    { "Toscana", new List<string> { "Firenze", "Pisa", "Siena", "Livorno", "Lucca", "Arezzo", "Pistoia", "Grosseto", "Prato", "Massa" } },
    { "Trentino-Alto Adige", new List<string> { "Trento", "Bolzano" } },
    { "Umbria", new List<string> { "Perugia", "Terni" } },
    { "Valle D'Aosta", new List<string> { "Aosta" } },
    { "Veneto", new List<string> { "Venezia", "Verona", "Padova", "Vicenza", "Treviso", "Rovigo", "Belluno" } }
};
            var cList = regions.Select(region => new CountryModel
            {
                Name = region.Key,
                Cities = region.Value
            }).ToList();
        RegionList = cList;

        }

    }
}
