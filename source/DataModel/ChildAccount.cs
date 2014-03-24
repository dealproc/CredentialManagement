namespace DataModel {
    public class ChildAccount
        : Account {
        public virtual Account MasterAccount { get; set; }
    }
}