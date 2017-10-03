using System;

namespace iAsset.WebApi.Models.Weather
{
    public class WeatherRequest
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }

    }
}