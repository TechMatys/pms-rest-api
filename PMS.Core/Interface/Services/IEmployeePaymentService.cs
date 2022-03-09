using PMS.Core.Model;


namespace PMS.Core.Interface.Services
{
    public interface IEmployeePaymentService
    {
        Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment();
        Task<EmployeePayment> GetEmployeePaymentById(int id);
        Task<int> Create(EmployeePayment fields);
        Task<int> Update(int id, EmployeePayment fields);
        Task<int> Delete(int id);
      
    }
}

