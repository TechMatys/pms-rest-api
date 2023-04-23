
namespace PMS.Core.Model
{
    public class CompanyExpenseListModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Amount { get; set; }
        public string? ExpenseDate { get; set; }
        public string? PaidBy { get; set; }
    }

    public class CompanyExpense
    {

        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? Amount { get; set; }
        public string? ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public int? PaidBy { get; set; }
        public int? ManagedBy { get; set; }

    }
}
