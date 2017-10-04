using iAsset.Services.DTO;

namespace iAsset.Services.Results
{
    public class WeatherResult : ServiceResult
    {
        public WeatherDto CityWeather { get; set; }

    }
}
