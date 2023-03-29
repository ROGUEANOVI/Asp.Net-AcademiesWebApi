using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Abstractions
{
    public interface ICrud<T>
    {
        Task<IList<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Insert(T entity);

        Task Delete(int id);

        Task Update(T entity);

    }
}
