using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class AccountRole
    {
        public virtual int Id { get; set; }
        public virtual Group Group { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
