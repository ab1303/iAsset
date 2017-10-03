using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using iAsset.Services.DTO;
using iAsset.Services.GlobalWeather;
using iAsset.Services.Interfaces;
using iAsset.Services.Results;

namespace iAsset.Services.Implementations
{
    public class WeatherService : IWeatherService
    {

        public CountryCityResult GetCities(string country)
        {

            try
            {
                using (var globalWeatherSoapClient = new GlobalWeatherSoapClient("GlobalWeatherSoap"))
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
    }
}
