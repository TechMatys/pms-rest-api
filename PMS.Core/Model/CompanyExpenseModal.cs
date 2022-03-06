
namespace PMS.Core.Model
{
    public class CompanyExpenseListModel
    {
        public int CompanyExpenseId { get; set; }
        public string? ExpenseName { get; set; }
        public int? Amount { get; set; }
        public string? ExpenseDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CompanyExpense
    {

        public int CompanyExpenseId { get; set; }
        public string? Title { get; set; }
        public int? Amount { get; set; }
        public string? ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public int? ManagedBy { get; set; }

    }
}
