using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeListModel>> GetAllEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<int> Create(Employee fields);
        Task<int> Update(int id, Employee fields);
        Task<int> Delete(int id);
    }
}
