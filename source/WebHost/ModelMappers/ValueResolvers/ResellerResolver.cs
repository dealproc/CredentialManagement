using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers.ValueResolvers
{
    public class ResellerResolver
        : ValueResolver<int, DataModel.Reseller>
    {
        IDataContext _DbContext;
        public ResellerResolver(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _DbContext = db;
        }
        protected override DataModel.Reseller ResolveCore(int source)
        {
            return _DbContext.Resellers.SingleOrDefault(x => x.Id == source);
        }
    }
}