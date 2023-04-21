using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeePaymentRepository
    {
        Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePaymentAsync();
        Task<EmployeePayment> GetEmployeePaymentByIdAsync(int id);
        Task<int?> CreateAsync(EmployeePayment fields);
        Task<int?> UpdateAsync(int id, EmployeePayment fields);
        Task<int?> DeleteAsync(int id);
    }
}
