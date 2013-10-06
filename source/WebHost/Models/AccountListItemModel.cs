
namespace WebHost.Models
{
    public class MasterAccountListItemModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CommonName { get; set; }
        public int ResellerId { get; set; }
        public string ResellerLegalName { get; set; }
        public string ResellerDBAName { get; set; }
        public string BillingAccountNumber { get; set; }
        public bool InvoiceReseller { get; set; }

        public class ChildAccountListItemModel
        {
            public int Id { get; set; }
            public string AccountNumber { get; set; }
            public string CommonName { get; set; }
        }
    }
}