using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Group
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string GroupKey { get; set; }
        public virtual string Description { get; set; }
        public virtual bool AutoAssign { get; set; }
        public virtual IList<Group> Parents { get; set; }
        public virtual IList<AccountRole> AccountRoles { get; set; }
        public virtual IList<User> Users { get; set; }
        public virtual IEnumerable<Group> AllParentGroups()
        {
            if (Parents == null)
            {
                Group[] result = { };
                return result;
            }

            return Parents.Union(
                Parents.SelectMany(p => p.AllParentGroups())
            );
        }


        public virtual void AddParents(IEnumerable<Group> parents)
        {
        }
        public virtual void AddParent(Group parent)
        {
        }
        public virtual void RemoveParents(IEnumerable<Group> parents)
        {
        }
        public virtual void RemoveParent(IEnumerable<Group> parent)
        {
        }

        // How do we do bulk adds/removes?

        public virtual void AddAccount(Account a, Role r)
        {
        }
        public virtual void RemoveAccount(Account a)
        {
        }

        public virtual void AddUsers(IEnumerable<User> users)
        {
        }
        public virtual void AddUser(User user)
        {
        }
        public virtual void RemoveUsers(IEnumerable<User> users)
        {
        }
        public virtual void RemoveUser(User user)
        {
        }
    }
}
