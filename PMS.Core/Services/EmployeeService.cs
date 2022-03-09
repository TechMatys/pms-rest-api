using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeService(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository ?? throw new ArgumentNullException(nameof(EmployeeRepository));
        }

        public async Task<IEnumerable<EmployeeListModel>> GetAllEmployee()
        {
            return await _EmployeeRepository.GetAllEmployee();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _EmployeeRepository.GetEmployeeById(id);
        }

        public async Task<int> Create(Employee fields)
        {
            return await _EmployeeRepository.Create(fields);
        }

        public async Task<int> Update(int id, Employee fields)
        {
            return await _EmployeeRepository.Update(id, fields);
        }

        public async Task<int> Delete(int id)
        {
            return await _EmployeeRepository.Delete(id);
        }
    }
}
