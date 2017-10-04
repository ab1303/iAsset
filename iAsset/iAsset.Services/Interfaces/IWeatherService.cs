using iAsset.Services.Results;

namespace iAsset.Services.Interfaces
{
    public interface IWeatherService
    {
        CountryCityResult GetCities(string country);
        WeatherResult GetCityWeather(string cityName);
    }
}
