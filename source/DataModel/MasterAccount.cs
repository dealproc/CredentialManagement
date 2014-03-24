namespace DataModel
{
    using System.Collections.Generic;
    public class MasterAccount
        : Account
    {
        public virtual Reseller Reseller { get; set; }
        public virtual IList<Account> SubAccounts { get; set; }
        public virtual string BillingAccountNumber { get; set; }
        public virtual bool InvoiceReseller { get; set; }
    }
}