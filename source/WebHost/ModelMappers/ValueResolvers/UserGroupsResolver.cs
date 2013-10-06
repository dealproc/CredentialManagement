using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers.ValueResolvers
{
    public class UserGroupsResolver
        : ValueResolver<DataModel.User, IEnumerable<DataModel.Group>>
    {
        IDataContext _DbContext;
        public UserGroupsResolver(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _DbContext = db;
        }
        protected override IEnumerable<DataModel.Group> ResolveCore(DataModel.User source)
        {
            var groups = _DbContext.Groups.All().Where(x => x.Users.Any(u => u.Id == source.Id));
            return groups.Union(groups.SelectMany(g => g.AllParentGroups()));
        }
    }
}