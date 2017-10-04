using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using iAsset.Services.Implementations;
using iAsset.Services.Results;
using NUnit.Framework;

namespace iAsset.Services.Tests
{
    [TestFixture(Category = "WeatherService")]
    public class GlobalWeatherTest
    {
        private WeatherService _weatherService;
        [SetUp]
        public void Setup()
        {
            var soapEndpointName = ConfigurationManager.AppSettings["gw:soap-endpoint-name"];
            var openWeatherMapUrl = ConfigurationManager.AppSettings["owm:url"];
            var openWeatherMapAppId = ConfigurationManager.AppSettings["owm:appid"];
            _weatherService = new WeatherService(soapEndpointName, openWeatherMapUrl, openWeatherMapAppId);
        }


        [Test]
        [TestCase("Australia", TestName = "webservice response cities")]
        public void webservice_response_of_cities_is_success_not_null_or_empty(string county)
        {
         
            var cityResult = _weatherService.GetCities(county);
            Assert.AreEqual(cityResult.Status, ServiceStatus.Success);

            var cityCollection = cityResult.Cities as string[] ?? cityResult.Cities.ToArray();
            Assert.IsNotNull(cityCollection);
            CollectionAssert.IsNotEmpty(cityCollection);

        }


        [Test]
        [TestCase("Australia", new string[] { "", "" }, TestName = "Australian Cities")]
        public void match_list_of_cities_found_in_response(string county, IEnumerable<string> cities)
        {
            var setupCities = new[]
            {
                "Archerfield Aerodrome",
                "Amberley Aerodrome",
                "Brisbane Airport M. O",
                "Coolangatta Airport Aws",
                "Cairns Airport",
            };

            var cityResult = _weatherService.GetCities(county).Cities;
            CollectionAssert.IsSubsetOf(setupCities, cityResult);
        }


        [Test]
        [TestCase("Sydney", TestName = "open weather map api response weather")]
        public void apiservice_response_of_weather_is_successful_and_not_null(string city)
        {

            var weatherResult = _weatherService.GetCityWeather(city);
            Assert.AreEqual(weatherResult.Status, ServiceStatus.Success);
            Assert.IsNotNull(weatherResult.CityWeather);

        }












    }
}
