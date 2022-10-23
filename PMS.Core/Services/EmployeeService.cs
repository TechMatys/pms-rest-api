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
            return await _employeeRepository.GetAllEmployee();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<int> Create(Employee fields)
        {
            return await _employeeRepository.Create(fields);
        }

        public async Task<int> Update(int id, Employee fields)
        {
            return await _employeeRepository.Update(id, fields);
        }

        public async Task<int> Delete(int id)
        {
            return await _employeeRepository.Delete(id);
        }


        public async Task<IEnumerable<EmployeeTaskListModel>> GetAllTaskDetails(int id)
        {
            return await _employeeRepository.GetAllTaskDetails(id);
        }

        public async Task<EmployeeTaskDetails> GetTaskDetailById(int id, int taskId)
        {
            return await _employeeRepository.GetTaskDetailById(id, taskId);
        }

        public async Task<int> CreateTask(int id, EmployeeTaskDetails fields)
        {
            return await _employeeRepository.CreateTask(id, fields);
        }

        public async Task<int> DeleteTask(int id, int taskId)
        {
            return await _employeeRepository.DeleteTask(id, taskId);
        }
    }
}
