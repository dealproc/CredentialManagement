using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class AccountRoleMap
        : ClassMap<AccountRole>
    {
        public AccountRoleMap()
        {
            Id(x => x.Id);

            References(x => x.Account);
            References(x => x.Group);
            References(x => x.Role);
        }
    }
}
