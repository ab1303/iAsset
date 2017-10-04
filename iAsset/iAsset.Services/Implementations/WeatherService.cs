using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using iAsset.Services.DTO;
using iAsset.Services.GlobalWeather;
using iAsset.Services.Interfaces;
using iAsset.Services.Model;
using iAsset.Services.Results;
using Newtonsoft.Json;

namespace iAsset.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly string _soapEndpointName;
        private readonly string _openWeatherMapUrl;
        private readonly string _openWeatherMapAppId;

        public WeatherService(string soapEndpointName, string openWeatherMapUrl, string openWeatherMapAppId)
        {
            _soapEndpointName = soapEndpointName;
            _openWeatherMapUrl = openWeatherMapUrl;
            _openWeatherMapAppId = openWeatherMapAppId;
        }

        public CountryCityResult GetCities(string country)
        {
            try
            {
                using (var globalWeatherSoapClient = new GlobalWeatherSoapClient(_soapEndpointName))
                {
                    var cityXmlString = globalWeatherSoapClient.GetCitiesByCountry(country);
                    if (string.IsNullOrEmpty(cityXmlString))
                        return new CountryCityResult
                        {
                            Exception = new InvalidDataException("Xml response is empty")
                        };


                    var serializer = new XmlSerializer(typeof(CityResult));
                    using (var reader = new StringReader(cityXmlString))
                    {
                        var cityResponse = serializer.Deserialize(reader) as CityResult;
                        if (cityResponse == null) return new CountryCityResult
                        {
                            Exception = new InvalidDataException("Error in deserializing xml respones")
                        };

                        return new CountryCityResult
                        {
                            Status = ServiceStatus.Success,
                            Cities = cityResponse.Tables.Select(t => t.City).ToList()
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                return new CountryCityResult
                {
                    Exception = ex
                };
            }
        }

        public WeatherResult GetCityWeather(string cityName)
        {
            try
            {
                var apiUrl = string.Format("{0}?q={1}&APPID={2}", _openWeatherMapUrl, cityName, _openWeatherMapAppId);
                var weatherCondition = new WeatherDto();
                using (var client = new HttpClient())
                {
                    var json = client.GetAsync(apiUrl).Result.Content.ReadAsStringAsync().Result;
                    var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
                    if (weatherResponse == null)
                        return new WeatherResult
                        {
                            Status = ServiceStatus.Success,
                            CityWeather = weatherCondition
                        };

                    weatherCondition.Location = weatherResponse.Name;
                    weatherCondition.Wind = weatherResponse.Wind.Speed;
                    weatherCondition.Visibility = weatherResponse.Visibility;
                    var skyCondition = weatherResponse.Weather.FirstOrDefault();
                    if (skyCondition != null)
                        weatherCondition.SkyConditions = skyCondition.Description;

                    weatherCondition.Temperature = weatherResponse.Main.Temp;
                    weatherCondition.DewPoint = weatherResponse.Main.Humidity;
                    weatherCondition.RelativeHumidity = weatherResponse.Main.Humidity;
                    weatherCondition.Pressure = weatherResponse.Main.Pressure;
                }

                return new WeatherResult
                {
                    Status = ServiceStatus.Success,
                    CityWeather = weatherCondition
                };

            }
            catch (Exception ex)
            {
                return new WeatherResult
                {
                    Exception = ex
                };

            }


        }
    }
}
