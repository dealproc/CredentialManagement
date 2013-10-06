using System;
using System.Collections.Generic;

namespace WebHost.Models
{
    public class UserListItemModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastActivityUTC { get; set; }
        public IEnumerable<GroupListModel> Groups { get; set; }

        public class GroupListModel
        {
            public string Description { get; set; }
            public IEnumerable<AccountRoleListModel> AccountRoles { get; set; }
        }

        public class AccountRoleListModel
        {
            public string AccountName { get; set; }
            public string RoleName { get; set; }
        }
    }
}