using MBKM.Common.Interfaces;
using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.BaseRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MBKMContext db;

        public GenericRepository(MBKMContext _db)
        {
            db = _db;
        }
        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            db.Set<T>().AddRange(entities);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate);
        }

        public T Get(object Id)
        {
            return db.Set<T>().Find(Id);
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }

        public void Remove(object Id)
        {
            T entity = db.Set<T>().Find(Id);
            db.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            db.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}
