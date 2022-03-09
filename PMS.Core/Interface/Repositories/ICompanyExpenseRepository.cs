using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface ICompanyExpenseRepository
    {
        Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpenses();
        Task<CompanyExpense> GetCompanyExpenseById(int id);
        Task<int> Create(CompanyExpense fields);
        Task<int> Update(int id, CompanyExpense fields);
        Task<int> Delete(int id);
    }
}
