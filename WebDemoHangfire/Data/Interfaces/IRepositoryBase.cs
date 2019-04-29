using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoHangfire.Data.Interfaces
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        List<T> GetAll();

        T GetById(string id);

        void Add(T obj);

        void Update(string id, T obj);

        void Remove(string id);

        int SaveChanges();
    }
}
