using AcademiesWebApi.Abstractions;
using AcademiesWebApi.Entities;
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

    public class DbContext<T> : IDbContext<T> where T : class
    {
        ApiDbContext _dbContext;
        
        DbSet<T> _EntitySet;

        public DbContext(ApiDbContext dbContext)
        { 
            _dbContext = dbContext;

            _EntitySet = dbContext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _EntitySet.FindAsync(id);

            _EntitySet.Remove(entity);
            await Save();
        }

        public async Task<IList<T>> GetAll()
        {
            return await _EntitySet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _EntitySet.FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            await _EntitySet.AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _EntitySet.Update(entity);
            await Save();
        }
    }
}
