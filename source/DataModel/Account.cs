namespace DataModel {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public abstract class Account {
        public virtual int Id { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string CommonName { get; set; }
        public virtual Address BillTo { get; set; }
        public virtual Address ShipTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account() {
            BillTo = new Address();
            ShipTo = new Address();
        }
    }
}