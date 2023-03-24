using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Abstractions
{
    public interface ICrud<T>
    {
        IList<T> GetAll();

        T GetById(int id);

        T Save(T entity);

        void Delete(int id);
    }
}
