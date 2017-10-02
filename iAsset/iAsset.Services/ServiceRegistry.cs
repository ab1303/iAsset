using System;
using IAsset.Data;
using IAsset.Services.Implementations;
using IAsset.Services.Interfaces;
using StructureMap;

namespace IAsset.Services
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry(IoC container)
        {

            For<IWeatherRepository>().Use<WeatherRepository>();

            IncludeRegistry(new DataRegistry());
            For<IoC>().Use(container);
        }
    }
}
