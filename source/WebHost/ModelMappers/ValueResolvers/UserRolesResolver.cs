using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers.ValueResolvers
{
    public class UserRolesResolver
        : ValueResolver<DataModel.User, IEnumerable<AccountRole>>
    {
        IDataContext _DbContext;
        public UserRolesResolver(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _DbContext = db;
        }
        protected override IEnumerable<AccountRole> ResolveCore(DataModel.User source)
        {
            var groups = _DbContext.Groups.All().Where(x => x.Users.Any(u => u.Id == source.Id));
            return groups.SelectMany(x => x.AccountRoles);
        }
    }
}