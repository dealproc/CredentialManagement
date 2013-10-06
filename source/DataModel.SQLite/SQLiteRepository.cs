using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite
{
    public class SQLiteRepository<T>
        : IRepository<T>
    {
        ISession _Session;
        Guid _Id = Guid.NewGuid();

        public IQueryable<T> All()
        {
            return _Session.Query<T>();
        }

        public IQueryable<T> All(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().Where(expression);
        }

        public bool Any(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().Any(expression);
        }

        public T First(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().First(expression);
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().FirstOrDefault(expression);
        }

        public T Single(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().Single(expression);
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _Session.Query<T>().SingleOrDefault(expression);
        }

        public IQueryable<T> OfType<T>()
        {
            return _Session.Query<T>();
        }

        public T Insert(T item)
        {
            if (!_Session.Transaction.IsActive)
            {
                _Session.BeginTransaction();
            }
            _Session.SaveOrUpdate(item);
            return item;
        }

        public T Update(T item)
        {
            if (!_Session.Transaction.IsActive)
            {
                _Session.BeginTransaction();
            }
            _Session.Update(item);
            return item;
        }

        public void Delete(T item)
        {
            if (!_Session.Transaction.IsActive)
            {
                _Session.BeginTransaction();
            }
            _Session.Delete(item);
        }

        public SQLiteRepository(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            _Session = session;
        }
    }
}
