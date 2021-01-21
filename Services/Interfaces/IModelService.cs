using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface IModelService<T> where T : class
    {
        List<T> Get(Func<T, bool> filter = null, string includeProperties = null);
        T Find(Func<T, bool> filter = null, string includeProperties = null);
        T Insert(T model);
        T Update(T model);
        int Delete(Func<T, bool> filter = null);
    }
}
