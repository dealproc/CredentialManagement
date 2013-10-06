using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.Models
{
    public class InheritRoleEditModel
    {
        public int RoleId { get; set; }
        public int[] IncludeRoleIds { get; set; }
    }
}