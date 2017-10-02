using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAsset.Services.Interfaces
{
    public interface IDataRepository<T, in TKey> 
      where T : class,  new()
    {
        T Add(T entity);
        void Remove(TKey id);
        T Update(T entity);
        IEnumerable<T> Get();
        T Get(TKey id);
    }
}
