using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<IEnumerable<EmployeeListModel>> GetAllEmployee()
        {
            return await _employeeRepository.GetAllEmployeeAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<int?> Create(Employee fields)
        {
            return await _employeeRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, Employee fields)
        {
            return await _employeeRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }


        public async Task<IEnumerable<EmployeeTaskListModel>> GetAllTaskDetails(int id)
        {
            return await _employeeRepository.GetAllTaskDetailsAsync(id);
        }

        public async Task<EmployeeTaskDetails> GetTaskDetailById(int id, int taskId)
        {
            return await _employeeRepository.GetTaskDetailByIdAsync(id, taskId);
        }

        public async Task<int?> CreateTask(int id, EmployeeTaskDetails fields)
        {
            return await _employeeRepository.CreateTaskAsync(id, fields);
        }

        public async Task<int?> DeleteTask(int id, int taskId)
        {
            return await _employeeRepository.DeleteTaskAsync(id, taskId);
        }
    }
}
