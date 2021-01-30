
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;


namespace Services.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal ModelDb context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ModelDb context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            context.ChangeTracker.LazyLoadingEnabled = true;
        }

        public virtual List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
                //return orderBy(query);
            }
            else
            {
                return query.ToList();
                //return query;
            }
        }
        public virtual List<object> Get(
             Expression<Func<TEntity, object>> selector)
        {
            IQueryable<TEntity> query = dbSet;


            return query.Select(selector).ToList();
        }
        public virtual IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != "")
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            return await query.ToListAsync();
        }
        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != "")
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return query.Where(filter).FirstOrDefault();
        }
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {

            return await dbSet.Where(filter).FirstOrDefaultAsync();
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void InsertRange(IEnumerable<TEntity> entity)
        {
            dbSet.AddRange(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            Delete(this.Get(filter));
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            var res = await this.FindAsync(filter);
            dbSet.Attach(res);
            dbSet.Remove(res);
            return await context.SaveChangesAsync();
        }
        public virtual void Delete(List<TEntity> entityToDelete)
        {
            foreach (var entity in entityToDelete)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            var entity = await dbSet.Where(filter).FirstOrDefaultAsync();
            return entity;
        }
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            dbSet.Add(entity);
            return await context.SaveChangesAsync();
        }
        public virtual async Task<TEntity> CloneAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }
        public virtual TEntity Clone(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != "")
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            return query.Where(filter).AsNoTracking().FirstOrDefault();
        }
        public void Detach(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        public void DetachRange(IEnumerable<TEntity> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }


}