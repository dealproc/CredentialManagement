using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class RoleMap
        : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.HasAllPermissions);

            HasMany(x => x.Permissions).Cascade.SaveUpdate();
            HasMany(x => x.IncludedRoles).Cascade.SaveUpdate();
        }
    }
}
