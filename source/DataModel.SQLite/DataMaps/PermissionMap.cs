using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class PermissionMap
        : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Id(x => x.Id);

            Map(x => x.Code);
            Map(x => x.Name);
        }
    }
}
