using MBKM.Common.Interfaces;
using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services.BaseServices
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;
        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public void Delete(int ID)
        {
            if (ID == 0)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Remove(ID);
            _unitOfWork.Commit();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public T Get(object Id)
        {
            return _repository.Get(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity parameter cannot be null");
            }
            //If the entity has an ID, we assume it exists in the DB
            if (entity.ID != 0)
            {
                DoUpdate(entity);

            }
            else
            {
                DoInsert(entity);
            }
        }
        protected virtual void DoInsert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }
        protected virtual void DoUpdate(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Update(entity);
            _unitOfWork.Commit();
        }
        public void SaveRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.AddRange(entities);
            _unitOfWork.Commit();
        }
    }
}
