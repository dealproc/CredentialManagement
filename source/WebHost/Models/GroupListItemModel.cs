using System.Collections.Generic;

namespace WebHost.Models
{
    public class GroupListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public IEnumerable<AccountAssignmentListItemModel> AccountAssignments { get; set; }

        public class AccountAssignmentListItemModel
        {
            public int Id { get; set; }
            public int AccountId { get; set; }
            public string AccountName { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}