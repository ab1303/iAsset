using System.Collections.Generic;
using System.Linq;
using iAsset.Services.Implementations;
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
            _weatherService = new WeatherService();
        }


        [Test]
        [TestCase("Australia", new string[] { "", "" }, TestName = "Australian Cities")]
        public void match_list_of_cities_found_in_response(string county, IEnumerable<string> cities)
        {
            var setupCities = new string[]
            {
                "Archerfield Aerodrome",
                "Amberley Aerodrome",
                "Brisbane Airport M. O",
                "Coolangatta Airport Aws",
                "Cairns Airport",
            };

            var cityResult = _weatherService.GetCities(county).Cities;

            Assert.IsNotNull(cityResult);

            var cityCollection = cityResult as string[] ?? cityResult.ToArray();
            CollectionAssert.IsNotEmpty(cityCollection);
            CollectionAssert.IsSubsetOf(setupCities, cityCollection);


        }
    }
}
