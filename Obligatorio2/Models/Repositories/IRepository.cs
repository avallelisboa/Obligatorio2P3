using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio2.Models.Repositories
{
    interface IRepository<T> where T:class
    {
        bool Add(T instance);
        bool Remove(object id);
        bool Update(T instance);
        T FindById(object id);
        List<T> FindAll();
    }
}