using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class MasterAccount
        : Account
    {
        public virtual Reseller Reseller { get; set; }
        public virtual IList<Account> SubAccounts { get; set; }
        public virtual string BillingAccountNumber { get; set; }
        public virtual bool InvoiceReseller { get; set; }
    }
}
