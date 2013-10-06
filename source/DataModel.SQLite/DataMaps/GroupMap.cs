using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class GroupMap
        : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.GroupKey);
            Map(x => x.Description);
            Map(x => x.AutoAssign);

            HasMany(x => x.AccountRoles);
            HasMany(x => x.Parents);
            HasMany(x => x.Users);
        }
    }
}
