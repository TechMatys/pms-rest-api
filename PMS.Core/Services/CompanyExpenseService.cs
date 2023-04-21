using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;


namespace PMS.Core.Services
{
    public class CompanyExpenseService : ICompanyExpenseService
    {
        public readonly ICompanyExpenseRepository _CompanyExpenseRepository;

        public CompanyExpenseService(ICompanyExpenseRepository CompanyExpenseRepository)
        {
            _CompanyExpenseRepository = CompanyExpenseRepository ?? throw new ArgumentNullException(nameof(CompanyExpenseRepository));
        }

        public async Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpenses()
        {
            return await _CompanyExpenseRepository.GetAllCompanyExpensesAsync();
        }

        public async Task<CompanyExpense> GetCompanyExpenseById(int id)
        {
            return await _CompanyExpenseRepository.GetCompanyExpenseByIdAsync(id);
        }

        public async Task<int?> Create(CompanyExpense fields)
        {
            return await _CompanyExpenseRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, CompanyExpense fields)
        {
            return await _CompanyExpenseRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _CompanyExpenseRepository.DeleteAsync(id);
        }
    }
}
