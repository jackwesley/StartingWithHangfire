using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoHangfire.Data
{
    public interface IDatabase<T> where T : class
    {
        List<T> GetAll();

        T GetById(string id);

        void Add(T todo);

        void Update(string id, T todo);

        void Remove(string id);
    }
}
