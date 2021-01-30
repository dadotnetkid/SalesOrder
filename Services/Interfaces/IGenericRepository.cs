using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        List<object> Get(
            Expression<Func<TEntity, object>> selector);
        IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        TEntity GetByID(object id);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null);
        TEntity Find(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entity);
        void Delete(object id);
        void Delete(Expression<Func<TEntity, bool>> filter);
        void Delete(List<TEntity> entityToDelete);
        void Update(TEntity entityToUpdate);
        Task<int> UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        Task<int> InsertAsync(TEntity entity);
        Task<TEntity> CloneAsync(Expression<Func<TEntity, bool>> filter);
        TEntity Clone(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        void Detach(TEntity entity);
        void DetachRange(IEnumerable<TEntity> entity);
        void SaveChanges();
    }
}
