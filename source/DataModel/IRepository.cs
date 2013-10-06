using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> All(Expression<Func<T, bool>> expression);
        IQueryable<T> OfType<T>();
        bool Any(Expression<Func<T, bool>> expression);
        T First(Expression<Func<T, bool>> expression);
        T FirstOrDefault(Expression<Func<T, bool>> expression);
        T Single(Expression<Func<T, bool>> expression);
        T SingleOrDefault(Expression<Func<T, bool>> expression);
        T Insert(T item);
        T Update(T item);
        void Delete(T item);
    }
}
