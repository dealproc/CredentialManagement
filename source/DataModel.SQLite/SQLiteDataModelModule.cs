using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite
{
    public class SQLiteDataModelModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>().ToProvider<NHibernateSessionFactoryProvider>().InRequestScope();
            Bind<ISession>().ToProvider<NHibernateSessionProvider>().InRequestScope();
            Bind(typeof(IRepository<>)).To(typeof(SQLiteRepository<>)).InRequestScope();
            Bind<IDataContext>().To<SQLiteDataContext>().InRequestScope();
        }
    }
}
