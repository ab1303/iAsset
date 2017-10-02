using System;
using System.Data.Entity;
using IAsset.Data.Models;


namespace IAsset.Data
{
    public interface IDataContext : IDisposable
    {
        IDbSet<Weather> Cheques { get; set; }
        int SaveChanges();
    }
}
