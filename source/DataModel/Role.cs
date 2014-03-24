namespace DataModel {
    using System.Collections.Generic;
    using System.Linq;
    public class Role {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual bool HasAllPermissions { get; set; }

        public virtual IList<Role> ParentRoles { get; set; }
        public virtual IList<Role> IncludedRoles { get; set; }

        public virtual IList<Permission> Permissions { get; set; }

        public virtual IEnumerable<Role> AllParentRoles() {
            if (IncludedRoles == null) {
                Role[] result = { };
                return result;
            }

            return IncludedRoles.Union(
                IncludedRoles.SelectMany(x => x.AllParentRoles())
            );
        }

        public virtual void AddRoles(IEnumerable<Role> roles) {
            foreach (var role in roles) {
                AddRole(role);
            }
        }
        public virtual void AddRole(Role r) {
            if (IncludedRoles == null) {
                IncludedRoles = new List<Role>();
            }

            if (!IncludedRoles.Any(role => role.Id == r.Id)) {
                IncludedRoles.Add(r);
            }
        }
        public virtual void RemoveRoles(IEnumerable<Role> roles) {
            foreach (var removal in roles) {
                RemoveRole(removal);
            }
        }
        public virtual void RemoveRole(Role r) {
            if (IncludedRoles == null) {
                return;
            }

            var removals = IncludedRoles.Where(role => role.Id == r.Id).ToArray();

            foreach (var removal in removals) {
                IncludedRoles.Remove(removal);
            }
        }

        public virtual void AddPermissions(IEnumerable<Permission> permissions) {
            foreach (var perm in permissions) {
                AddPermission(perm);
            }
        }
        public virtual void AddPermission(Permission p) {
            if (Permissions == null) {
                Permissions = new List<Permission>();
            }

            if (!Permissions.Any(perm => perm.Id == p.Id)) {
                Permissions.Add(p);
            }
        }
        public virtual void RemovePermissions(IEnumerable<Permission> permissions) {
            foreach (var perm in permissions) {
                RemovePermission(perm);
            }
        }
        public virtual void RemovePermission(Permission p) {
            if (Permissions == null) {
                return;
            }

            var removals = Permissions.Where(perm => perm.Id == p.Id).ToArray();

            foreach (var removal in removals) {
                Permissions.Remove(removal);
            }
        }
    }
}