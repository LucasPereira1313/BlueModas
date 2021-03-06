﻿using System.Collections.Generic;

namespace BlueModas.Interface
{
    public interface IPersist<T>
    {
        bool Save(T obj);
        bool Delete(T obj);
        T Get(object id);
        List<T> GetList();
        List<T> GetList(object obj);
    }
}
