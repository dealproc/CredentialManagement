
namespace WebHost.Models
{
    public class MasterAccountEditModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CommonName { get; set; }
        public Address BillTo { get; set; }
        public bool UseBillToForShipTo { get; set; }
        public Address ShipTo { get; set; }

        public int ResellerId { get; set; }
        public string BillingAccountNumber { get; set; }
        public bool InvoiceReseller { get; set; }

        public MasterAccountEditModel()
        {
            UseBillToForShipTo = true;
        }
    }
}