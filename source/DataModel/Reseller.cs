namespace DataModel {
    using System.Collections.Generic;
    public class Reseller {
        int _Id;
        string _LegalName;
        string _DoingBusinessAsName;
        string _ContactFirst;
        string _ContactLast;
        string _PhoneNumber;
        string _FaxNumber;
        string _Email;
        bool _InvoiceAccounts;
        string _BillingAccountNumber;
        Address _BillTo;
        Address _ShipTo;
        ICollection<Account> _Accounts;
        bool _IsActive;

        public virtual int Id {
            get { return _Id; }
            set { _Id = value; }
        }
        public virtual string LegalName {
            get { return _LegalName; }
            set { _LegalName = value; }
        }
        public virtual string DoingBusinessAsName {
            get { return _DoingBusinessAsName; }
            set { _DoingBusinessAsName = value; }
        }
        public virtual string ContactFirst {
            get { return _ContactFirst; }
            set { _ContactFirst = value; }
        }
        public virtual string ContactLast {
            get { return _ContactLast; }
            set { _ContactLast = value; }
        }
        public virtual string PhoneNumber {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        public virtual string FaxNumber {
            get { return _FaxNumber; }
            set { _FaxNumber = value; }
        }
        public virtual string Email {
            get { return _Email; }
            set { _Email = value; }
        }
        public virtual bool InvoiceAccounts {
            get { return _InvoiceAccounts; }
            set { _InvoiceAccounts = value; }
        }
        public virtual string BillingAccountNumber {
            get { return _BillingAccountNumber; }
            set { _BillingAccountNumber = value; }
        }
        public virtual Address BillTo {
            get { return _BillTo; }
            set { _BillTo = value; }
        }
        public virtual Address ShipTo {
            get { return _ShipTo; }
            set { _ShipTo = value; }
        }
        public virtual ICollection<Account> Accounts {
            get { return _Accounts; }
            set { _Accounts = value; }
        }
        public virtual bool IsActive {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}