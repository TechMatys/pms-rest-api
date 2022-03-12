

namespace PMS.Core.Model
{
    public class EmployeePaymentListModel
    {


        public int EmployeePaymentId { get; set; }
        public string? EmployeeName { get; set; }      
        public int? Amount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public string? PaymentDate { get; set; }
        public string? CreatedBy { get; set; }

    }

    public class EmployeePayment
    {

        public int EmployeeId { get; set; }
        public int EmployeePaymentId { get; set; }       
        public int? Amount { get; set; }
        public string? PaymentMonthYear { get; set; }
        public int? PaymentMonth { get; set; }
        public int? PaymentYear { get; set; }
        public string? PaymentDate { get; set; }
        public string? Notes { get; set; }
        public int? ManagedBy { get; set; }

    }
}
