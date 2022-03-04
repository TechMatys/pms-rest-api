

namespace PMS.Core.Model
{
    public class EmployeePaymentListModel
    {

        public int EmployeeId { get; set; }
        public string EmployeName { get; set; }
      
        public int? Amount { get; set; }
        public string? PaymenMonth { get; set; }
        public string? PaymentYear { get; set; }
        public string? PaymentDate { get; set; }
        
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
    }

    public class EmployeePayment
    {

        public int EmployeeId { get; set; }
        public int EmployeePaymentId { get; set; }
       
        public int? Amount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public string? PaymentDate { get; set; }
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
    }
}
