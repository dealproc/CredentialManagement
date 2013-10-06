using NHibernate;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite
{
    class NHibernateSessionProvider
        : Provider<ISession>
    {
        public NHibernateSessionProvider(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null)
            {
                throw new ArgumentNullException("sessionFactory");
            }

            _SessionFactory = sessionFactory;
        }

        private ISessionFactory _SessionFactory;
        private ISession _Session;

        protected override ISession CreateInstance(IContext context)
        {
            if (_Session == null)
            {
                _Session = _SessionFactory.OpenSession();
            }
            return _Session;
        }
    }
}
