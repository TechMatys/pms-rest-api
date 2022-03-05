

namespace PMS.Core.Model
{
    public class ProjectPaymentListModel
    {
        public int ProjectName { get; set; }
        public string? ReceivedAmount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public string? PaymentDate { get; set; }
    }

    public class ProjectPayment
    {
        public int ProjectId { get; set; }
        public string? ReceivedAmount { get; set; }
        public string? BalancedAmount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public string? PaymentDate { get; set; }
        
        
    }
}
