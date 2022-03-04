using PMS.Core.Model;
using PMS.Core.Services;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeePaymentRepository
    {
        Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment();
        Task<EmployeePayment> GetEmployeePaymentById(int id);
        Task<bool> Create(EmployeePayment fields);
        Task<bool> Update(int id, EmployeePayment fields);
        Task<bool> Delete(int id);
    }
}
