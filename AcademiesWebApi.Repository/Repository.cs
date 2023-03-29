using AcademiesWebApi.Abstractions;
using AcademiesWebApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Repository
{
    public interface IRepository<T> : ICrud<T>
    {
    }

    public class Repository<T> : IRepository<T>
    {

        IDbContext<T> _context;

        public Repository(IDbContext<T> context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Delete(id);
        }

        public async Task<IList<T>> GetAll()
        {
            return await _context.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.GetById(id);
        }

        public async Task<T> Insert(T entity)
        {
            return await _context.Insert(entity);
        }

        public async Task Update(T entity)
        {
            await _context.Update(entity);
        }
    }
}
