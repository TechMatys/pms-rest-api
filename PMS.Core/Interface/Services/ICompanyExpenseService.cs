using PMS.Core.Model;


namespace PMS.Core.Interface.Services
{
    internal interface ICompanyExpenseService
    {
        Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpense();
        Task<CompanyExpense> GetCompanyExpenseById(int id);
        Task<bool> Create(CompanyExpense fields);
        Task<bool> Update(int id, CompanyExpense fields);
        Task<bool> Delete(int id);
    }
}
