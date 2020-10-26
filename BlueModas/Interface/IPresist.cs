using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Interface
{
    public interface IPresist<T>
    {
        bool Save(T obj);
        bool Delete(T obj);
        T Get(object id);
        List<T> GetList();
        List<T> GetList(object obj);
    }
}
