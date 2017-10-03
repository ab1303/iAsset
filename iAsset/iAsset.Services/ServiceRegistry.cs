using System;
using iAsset.Services.Implementations;
using iAsset.Services.Interfaces;
using StructureMap;

namespace iAsset.Services
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry(IoC container)
        {

            For<IWeatherService>().Use<WeatherService>();

            For<IoC>().Use(container);
        }
    }
}
