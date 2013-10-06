using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Reseller
    {
        public virtual int Id { get; set; }
        public virtual string LegalName { get; set; }
        public virtual string DBAName { get; set; }
        public virtual Address BillTo { get; set; }
        public virtual Address ShipTo { get; set; }
        public virtual string BillingAccountNumber { get; set; }
    }
}
