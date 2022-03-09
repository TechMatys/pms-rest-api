using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeePaymentRepository
    {
        Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment();
        Task<EmployeePayment> GetEmployeePaymentById(int id);
        Task<int> Create(EmployeePayment fields);
        Task<int> Update(int id, EmployeePayment fields);
        Task<int> Delete(int id);
    }
}
