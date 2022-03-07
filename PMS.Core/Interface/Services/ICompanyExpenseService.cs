using PMS.Core.Model;


namespace PMS.Core.Interface.Services
{
    public interface ICompanyExpenseService
    {
        Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpenses();
        Task<CompanyExpense> GetCompanyExpenseById(int id);
        Task<bool> Create(CompanyExpense fields);
        Task<bool> Update(int id, CompanyExpense fields);
        Task<bool> Delete(int id);
    }
}
