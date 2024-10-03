using System.Reflection.Metadata;
using System.Net.Http.Json;
using WeatherApp.Models;
namespace WeatherApp.Services;
public class WeatherApiService
{



    //--------
    private readonly string API_KEY = "fe2d2ac6a671422fbd2136dc1578dc83";

    public const string ClientName = "WeatherClientService";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    public WeatherApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    private HttpClient httpClient => _httpClientFactory.CreateClient(ClientName);

    public async Task<WeatherApiResponse> GetWeatherInformation(string latitude, string longitude)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return null;
        return await httpClient.GetFromJsonAsync<WeatherApiResponse>($"current?access_key={API_KEY}&query={latitude},{longitude}");
    }
}