using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ChildAccount
        : Account
    {
        public virtual Account MasterAccount { get; set; }
    }
}
