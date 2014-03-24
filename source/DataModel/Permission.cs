namespace DataModel {
    using System.Collections.Generic;
    public class Permission {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}