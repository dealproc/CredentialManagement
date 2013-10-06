using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class UserMap
        : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.Username);
            Map(x => x.Email);
            Map(x => x.LastActivityUTC);
        }
    }
}
