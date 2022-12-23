using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;


namespace PMS.Core.Services
{
    public class EmployeePaymentService : IEmployeePaymentService
    {
        public readonly IEmployeePaymentRepository _EmployeePaymentRepository;

        public EmployeePaymentService(IEmployeePaymentRepository EmployeePaymentRepository)
        {
            _EmployeePaymentRepository = EmployeePaymentRepository ?? throw new ArgumentNullException(nameof(EmployeePaymentRepository));
        }

        public async Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment()
        {
            return await _EmployeePaymentRepository.GetAllEmployeePaymentAsync();
        }

        public async Task<EmployeePayment> GetEmployeePaymentById(int id)
        {
            return await _EmployeePaymentRepository.GetEmployeePaymentByIdAsync(id);
        }

        public async Task<int?> Create(EmployeePayment fields)
        {
            return await _EmployeePaymentRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, EmployeePayment fields)
        {
            return await _EmployeePaymentRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _EmployeePaymentRepository.DeleteAsync(id);
        }

       
    }

    
}
