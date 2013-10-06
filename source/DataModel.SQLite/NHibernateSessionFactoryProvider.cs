using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite
{
    public class NHibernateSessionFactoryProvider
        : Provider<ISessionFactory>
    {
        const string _DbFile = "c:\\temp\\test.db3";
        private static ISessionFactory _SessionFactory;

        protected override ISessionFactory CreateInstance(IContext context)
        {
            if (_SessionFactory == null)
            {
                var cfg = Fluently.Configure()
                    .Database(
                        SQLiteConfiguration.Standard
                            .Dialect<NHibernate.Dialect.SQLiteDialect>()
                            .ConnectionString(string.Format("Data Source={0};Version=3;PRAGMA synchronous=off;Enlist=no;Pooling=false;Max Pool Size=100;", _DbFile))
                            .IsolationLevel(IsolationLevel.ReadCommitted)
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionFactoryProvider>())
                    .CurrentSessionContext(typeof(ThreadStaticSessionContext).FullName)
                    .ExposeConfiguration(BuildSchema);

                _SessionFactory = cfg.BuildSessionFactory();
            }
            return _SessionFactory;
        }

        static void BuildSchema(Configuration config)
        {
            if (!File.Exists(_DbFile))
            {
                new SchemaExport(config).Create(false, true);
            }
        }
    }
}
