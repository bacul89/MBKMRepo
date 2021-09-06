using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces
{
    public interface IEntityService<T>
    {
        //void Delete(T entity);
        void Delete(int ID);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Get(object Id);
        void Save(T entity);
        void SaveRange(IEnumerable<T> entities);
    }
}
