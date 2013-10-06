using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite
{
    public class SQLiteDataContext
        : IDataContext
    {
        ISession _Session;
        IRepository<Account> _Accounts;
        IRepository<Group> _Groups;
        IRepository<Permission> _Permissions;
        IRepository<Role> _Roles;
        IRepository<User> _Users;
        IRepository<Reseller> _Resellers;

        public IRepository<Account> Accounts
        {
            get { return _Accounts; }
        }
        public IRepository<Group> Groups
        {
            get { return _Groups; }
        }
        public IRepository<Permission> Permissions
        {
            get { return _Permissions; }
        }
        public IRepository<Role> Roles
        {
            get { return _Roles; }
        }
        public IRepository<User> Users
        {
            get { return _Users; }
        }
        public IRepository<Reseller> Resellers
        {
            get { return _Resellers; }
        }

        public bool Commit()
        {
            try
            {
                if (_Session.Transaction.IsActive)
                {
                    _Session.Transaction.Commit();
                }
                _Session.Flush();
                Reconnect();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            return true;
        }

        public bool Rollback()
        {
            if (_Session.Transaction != null)
            {
                if (_Session.Transaction.IsActive && !_Session.Transaction.WasRolledBack)
                {
                    _Session.Transaction.Rollback();
                }

                _Session.Transaction.Dispose();
            }

            _Session.BeginTransaction();

            return true;
        }

        public bool Reconnect()
        {
            if (_Session.Transaction != null)
            {
                if (_Session.Transaction.IsActive)
                {
                    _Session.Transaction.Dispose();
                }
            }
            _Session.Clear();
            _Session.BeginTransaction();
            return true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _Session.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public SQLiteDataContext(
            IRepository<Account> accounts,
            IRepository<Group> groups,
            IRepository<Permission> permissions,
            IRepository<Role> roles,
            IRepository<User> users,
            IRepository<Reseller> resellers,
            ISession session
        )
        {
            _Accounts = accounts;
            _Groups = groups;
            _Permissions = permissions;
            _Roles = roles;
            _Users = users;
            _Resellers = resellers;

            _Session = session;
        }
    }
}
