using iAsset.Services.Implementations;
using iAsset.Services.Interfaces;
using StructureMap;

namespace iAsset.Services
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry(IoC container, string soapEndpointName, string openWeatherMapUrl, string openWeatherMapAppId)
        {            
            For<IWeatherService>()
                .Use<WeatherService>()
                .Ctor<string>("soapEndpointName").Is(soapEndpointName)
                .Ctor<string>("openWeatherMapUrl").Is(openWeatherMapUrl)
                .Ctor<string>("openWeatherMapAppId").Is(openWeatherMapAppId)
                ;

            For<IoC>().Use(container);
        }
    }
}
