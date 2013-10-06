
namespace WebHost.Models
{
    public class ChildAccountEditModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CommonName { get; set; }
        public Address BillTo { get; set; }
        public bool UseBillToForShipTo { get; set; }
        public Address ShipTo { get; set; }

        public int MasterAccountId { get; set; }

        public ChildAccountEditModel()
        {
            UseBillToForShipTo = true;
        }
    }
}