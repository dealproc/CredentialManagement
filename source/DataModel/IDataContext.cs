using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IDataContext 
        : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Group> Groups { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<Reseller> Resellers { get; }

        bool Commit();
        bool Rollback();
        bool Reconnect();
    }
}
