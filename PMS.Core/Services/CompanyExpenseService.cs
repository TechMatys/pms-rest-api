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
            return await _CompanyExpenseRepository.GetAllCompanyExpenses();
        }

        public async Task<CompanyExpense> GetCompanyExpenseById(int id)
        {
            return await _CompanyExpenseRepository.GetCompanyExpenseById(id);
        }

        public async Task<bool> Create(CompanyExpense fields)
        {
            return await _CompanyExpenseRepository.Create(fields);
        }

        public async Task<bool> Update(int id, CompanyExpense fields)
        {
            return await _CompanyExpenseRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _CompanyExpenseRepository.Delete(id);
        }
    }
}
