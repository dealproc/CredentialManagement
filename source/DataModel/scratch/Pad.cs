using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.scratch
{
    public class Pad
    {
        IDataContext Db { get; set; }

        User TheUser { get; set; }
        Account TheAccount { get; set; }

        public Pad()
        {
            // to get all groups for a user:
            var assignedGroups = Db.Groups.All(g => g.Users.Any(u => u.Id == TheUser.Id));
            var usersGroups = assignedGroups.SelectMany(g => g.AllParentGroups()).Distinct();

            // to get all roles for the user for an account:
            var allRoles = usersGroups
                .SelectMany(g => g.AccountRoles.Where(ar => ar.Account.Id == TheAccount.Id).Select(ar=>ar.Role))
                .Distinct();

            // to get a users claims:
            IQueryable<Permission> allPermissions;

            if (allRoles.Any(r => r.HasAllPermissions))
            {
                // if a role has the "all permissions" flag set.
                allPermissions = Db.Permissions.All();
            }
            else
            {
                // all other roles.
                allPermissions = allRoles.SelectMany(r => r.Permissions).Distinct();
            }

            var allClaims = allPermissions.Select(p => p.Code);
        }
    }
}
