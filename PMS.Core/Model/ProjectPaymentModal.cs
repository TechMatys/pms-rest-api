

namespace PMS.Core.Model
{
    public class ProjectPaymentListModel
    {
        public int ProjectPaymentId { get; set; }
        public string? ProjectName { get; set; }
        public long? ReceivedAmount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public string? PaymentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class ProjectPayment
    {
        public int ProjectPaymentId { get; set; }
        public int ProjectId { get; set; }
        public string? ReceivedAmount { get; set; }
        public string? BalancedAmount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public int? PaymentMonth { get; set; }
        public int? PaymentYear { get; set; }
        public string? PaymentDate { get; set; }
        public string? Notes { get; set; }
        public int? ManagedBy { get; set; }

    }
}
