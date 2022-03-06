using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IComapnyExpenseRepository
    {
        Task<IEnumerable<ComapnyExpenseListModel>> GetAllComapnyExpense();
        Task<ComapnyExpense> GetComapnyExpenseById(int id);
        Task<bool> Create(ComapnyExpense fields);
        Task<bool> Update(int id, ComapnyExpense fields);
        Task<bool> Delete(int id);
    }
}
