using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemoHangfire.Data.Interfaces;

namespace WebDemoHangfire.Data.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T obj)
        {
            _dbSet.Add(obj);
            SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(string id)
        {
            var objToRemove = GetById(id);
            _dbSet.Remove(objToRemove);
            SaveChanges();
        }

        public void Update(string id, T obj)
        {
            _dbSet.Update(obj);
            SaveChanges();
        }

        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
