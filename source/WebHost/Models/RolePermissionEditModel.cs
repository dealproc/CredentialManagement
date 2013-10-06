using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.Models
{
    public class RolePermissionEditModel
    {
        public int RoleId { get; set; }
        public int[] PermissionIds { get; set; }
    }
}