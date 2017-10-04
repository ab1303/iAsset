using iAsset.Services.DTO;

namespace iAsset.WebApi.Models.iAsset.Weather
{

    public class WeatherResponse : BaseApiResponse
    {
        public WeatherDto Weather { get; set; }

    }

   }