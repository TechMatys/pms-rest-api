using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeListModel>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<int?> CreateAsync(Employee fields);
        Task<int?> UpdateAsync(int id, Employee fields);
        Task<int?> DeleteAsync(int id);

        Task<IEnumerable<EmployeeTaskListModel>> GetAllTaskDetailsAsync(int id);
        Task<EmployeeTaskDetails> GetTaskDetailByIdAsync(int id, int taskId);
        Task<int?> CreateTaskAsync(int id, EmployeeTaskDetails fields);
        Task<int?> DeleteTaskAsync(int id, int taskId);
    }
}
