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
    }
}
