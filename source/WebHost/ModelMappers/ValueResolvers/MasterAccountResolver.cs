using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers.ValueResolvers
{
    public class MasterAccountResolver
        : ValueResolver<int, DataModel.MasterAccount>
    {
        IDataContext _DbContext;
        public MasterAccountResolver(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _DbContext = db;
        }
        protected override DataModel.MasterAccount ResolveCore(int source)
        {
            return _DbContext.Accounts
                .OfType<DataModel.MasterAccount>()
                .SingleOrDefault(x => x.Id == source);
        }
    }
}