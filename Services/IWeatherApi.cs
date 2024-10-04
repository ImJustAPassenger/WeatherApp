using Refit;
using WeatherApp.Models;

namespace WeatherApp.Services;
public interface IWeatherApi
{
    [Get("/current")]
    Task<WeatherApiResponse> GetCurrentAsync(string access_key,string[] query);

}