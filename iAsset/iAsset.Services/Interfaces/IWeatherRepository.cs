using IAsset.Services.DTO;

namespace IAsset.Services.Interfaces
{
    public interface IWeatherRepository : IDataRepository<WeatherDto, int>
    {
    }
}
