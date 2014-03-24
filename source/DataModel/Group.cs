namespace DataModel {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Group {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string GroupKey { get; set; }
        public virtual string Description { get; set; }
        public virtual bool AutoAssign { get; set; }
        public virtual IList<Group> Parents { get; set; }
        public virtual IList<Group> Children { get; set; }
        public virtual IList<AccountRole> AccountRoles { get; set; }
        public virtual IList<User> Users { get; set; }
        public virtual IEnumerable<Group> AllParentGroups() {
            if (Parents == null) {
                Group[] result = { };
                return result;
            }

            return Parents.Union(
                Parents.SelectMany(p => p.AllParentGroups())
            );
        }

        public virtual void AddParents(IEnumerable<Group> parents) {
            foreach (var parent in parents) {
                AddParent(parent);
            }
        }
        public virtual void AddParent(Group parent) {
            if (Parents == null) {
                Parents = new List<Group>();
            }

            if (!Parents.Any(p => p.Id == parent.Id)) {
                Parents.Add(parent);
            }
        }
        public virtual void RemoveParents(IEnumerable<Group> parents) {
            foreach (var removal in parents) {
                RemoveParent(removal);
            }
        }
        public virtual void RemoveParent(Group parent) {
            if (Parents == null) {
                return;
            }

            var removals = Parents.Where(p => p.Id == parent.Id).ToArray();

            foreach (var removal in removals) {
                Parents.Remove(removal);
            }
        }

        public virtual void AddAccounts(IEnumerable<Account> accounts, IEnumerable<Role> roles) {
            foreach (var account in accounts) {
                foreach (var role in roles) {
                    AddAccount(account, role);
                }
            }
        }
        public virtual void AddAccount(Account a, Role r) {
            if (AccountRoles == null) {
                AccountRoles = new List<AccountRole>();
            }

            if (!AccountRoles.Any(ar => ar.Account.Id == a.Id && ar.Role.Id == r.Id)) {
                AccountRoles.Add(new AccountRole() {
                    Group = this,
                    Account = a,
                    Role = r
                });
            }
        }
        public virtual void RemoveAccounts(IEnumerable<Account> accounts) {
            foreach (var account in accounts) {
                RemoveAccount(account);
            }
        }
        public virtual void RemoveAccount(Account a) {
            if (AccountRoles == null) {
                return;
            }

            var removals = AccountRoles.Where(role => role.Account.Id == a.Id).ToArray();

            foreach (var removal in removals) {
                AccountRoles.Remove(removal);
            }
        }
        public virtual void RemoveAccounts(Func<AccountRole, bool> query) {
            var removals = AccountRoles.Where(query).ToArray();
            foreach (var removal in removals) {
                AccountRoles.Remove(removal);
            }
        }

        public virtual void AddUsers(IEnumerable<User> users) {
            foreach (var user in users) {
                AddUser(user);
            }
        }
        public virtual void AddUser(User user) {
            if (Users == null) {
                Users = new List<User>();
            }

            if (!Users.Any(u => u.Id == user.Id)) {
                Users.Add(user);
            }
        }
        public virtual void RemoveUsers(IEnumerable<User> users) {
            foreach (var user in users) {
                RemoveUser(user);
            }
        }
        public virtual void RemoveUser(User user) {
            if (Users == null) {
                return;
            }

            var removals = Users.Where(u => u.Id == user.Id).ToArray();

            foreach (var removal in removals) {
                Users.Remove(user);
            }
        }
    }
}