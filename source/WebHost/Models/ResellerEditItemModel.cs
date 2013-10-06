
namespace WebHost.Models
{
    public class ResellerEditItemModel
    {
        public int Id { get; set; }
        public string LegalName { get; set; }
        public string DBAName { get; set; }
        public string BillingAccountNumber { get; set; }
        public Address BillTo { get; set; }
        public bool UseBillToForShipTo { get; set; }
        public Address ShipTo { get; set; }
    }
}