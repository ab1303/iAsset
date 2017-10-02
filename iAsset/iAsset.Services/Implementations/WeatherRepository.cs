
using System.Collections.Generic;
using System.Linq;
using IAsset.Data;
using IAsset.Data.Models;
using IAsset.Services.DTO;
using IAsset.Services.Interfaces;

namespace IAsset.Services.Implementations
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IDataContext _dataContext;

        public WeatherRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public WeatherDto Add(WeatherDto entity)
        {

            var chequeEntity = new Weather
            {
                WeatherId = entity.ChequeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Amount = entity.Amount,
                DateCreated = entity.DateCreated,
                DateChanged = entity.DateChanged
            };

            _dataContext.Cheques.Add(chequeEntity);
            _dataContext.SaveChanges();


            return new WeatherDto
            {
                ChequeId = chequeEntity.WeatherId,                
                FirstName = chequeEntity.FirstName,
                LastName = chequeEntity.LastName,
                Amount = chequeEntity.Amount,
                DateCreated = chequeEntity.DateCreated,
                DateChanged = chequeEntity.DateChanged

            };

        }

        public void Remove(int id)
        {
            var contactEntity = _dataContext.Cheques.SingleOrDefault(g => g.WeatherId == id);
            if (contactEntity == null)
                return;

            _dataContext.Cheques.Remove(contactEntity);
            _dataContext.SaveChanges();

        }

        public WeatherDto Update(WeatherDto entity)
        {
            var contactEntity = _dataContext.Cheques.SingleOrDefault(g => g.WeatherId == entity.ChequeId);
            if (contactEntity == null)
                return null;

            contactEntity.LastName = entity.LastName;
            contactEntity.FirstName = entity.FirstName;
            contactEntity.Amount = entity.Amount;

            _dataContext.SaveChanges();

            return new WeatherDto
            {
                ChequeId = contactEntity.WeatherId,
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                Amount = contactEntity.Amount,
                DateChanged = contactEntity.DateChanged,
                DateCreated = contactEntity.DateCreated,
            };
            
        }

        public IEnumerable<WeatherDto> Get()
        {
            return _dataContext.Cheques.Select(g => new WeatherDto
            {
                ChequeId = g.WeatherId,
                LastName = g.LastName,
                FirstName = g.FirstName,
                Amount = g.Amount,
                DateCreated = g.DateCreated,
                DateChanged = g.DateChanged
            }).ToList();
        }

        public WeatherDto Get(int id)
        {
            var chequeEntity = _dataContext.Cheques.SingleOrDefault(g => g.WeatherId == id);
            if (chequeEntity == null)
                return null;
            return new WeatherDto
            {
                ChequeId = chequeEntity.WeatherId,
                FirstName = chequeEntity.FirstName,
                LastName = chequeEntity.LastName,
                Amount = chequeEntity.Amount,
                DateCreated = chequeEntity.DateCreated,
                DateChanged = chequeEntity.DateChanged
            };
        }

    }
}
