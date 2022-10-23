using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListModel>> GetAllEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<int> Create(Employee fields);
        Task<int> Update(int id, Employee fields);
        Task<int> Delete(int id);
        Task<IEnumerable<EmployeeTaskListModel>> GetAllTaskDetails(int id);
        Task<EmployeeTaskDetails> GetTaskDetailById(int id, int taskId);
        Task<int> CreateTask(int id, EmployeeTaskDetails fields);
        Task<int> DeleteTask(int id, int taskId);
    }
}
