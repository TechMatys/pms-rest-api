
namespace PMS.Core.Model
{
    public class CompanyInvoiceListModel
    {
        public int CompanyInvoiceId { get; set; }
        public string? Title { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CompanyInvoice
    {

        public int CompanyInvoiceId { get; set; }
        public string? Title { get; set; }
        public int? ManagedBy { get; set; }

    }
}
