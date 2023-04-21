using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface ICompanyExpenseRepository
    {
        Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpensesAsync();
        Task<CompanyExpense> GetCompanyExpenseByIdAsync(int id);
        Task<int?> CreateAsync(CompanyExpense fields);
        Task<int?> UpdateAsync(int id, CompanyExpense fields);
        Task<int?> DeleteAsync(int id);
    }
}
