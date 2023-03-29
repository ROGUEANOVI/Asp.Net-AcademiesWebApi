using AcademiesWebApi.Abstractions;
using AcademiesWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Application
{
    public interface IApplication<T> : ICrud<T>
    {
    }

    public class Application<T> : IApplication<T>
    {
        IRepository<T> _repository;
        public Application(IRepository<T> repository)
        {
            _repository = repository; 
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<IList<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T> Insert(T entity)
        {
            return await _repository.Insert(entity);
        }

        public async Task Update(T entity)
        {
            await _repository.Update(entity);
        }
    }
}
