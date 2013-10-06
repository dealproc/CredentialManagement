using System.Collections.Generic;

namespace WebHost.Models
{
    public class RoleListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public IEnumerable<InheritedRole> IncludedRoles { get; set; }
        public IEnumerable<PermissionListItem> Permissions { get; set; }

        public class InheritedRole
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class PermissionListItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}