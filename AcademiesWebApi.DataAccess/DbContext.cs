using AcademiesWebApi.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcademiesWebApi.DataAccess
{
    public interface IDbContext<T> : ICrud<T>
    {

    }

    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        ApiDbContext _dbContext;
        
        DbSet<T> _items;

        //protected ApiDbContext _dbContext;
        public DbContext(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

            _items = dbContext.Set<T>();
        }

        public void Delete(int id)
        {
            var  entity = _items.Where(e => e.Id.Equals(id)).FirstOrDefault();
            if (entity != null)
            {
                _items.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Where(e => e.Id.Equals(id)).FirstOrDefault();
        }

        public T Save(T entity)
        {   
            _items.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

    }
}
