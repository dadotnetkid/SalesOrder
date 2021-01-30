using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface IModelService<T> where T : class
    {
        IEnumerable<T> Get(Func<T, bool> filter = null);
        IQueryable<T> Fetch(Expression<Func<T, T>> @select,
            Expression<Func<T, bool>> filter = null);

        T Find(Expression<Func<T, bool>> filter = null);

        T Insert(T model);
        T Update(T model);
        int Delete(Func<T, bool> filter = null);
    }
}
