using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeeProjectRepository
    {
        Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProject();
        Task<EmployeeProject> GetEmployeeProjectById(int id);
        Task<int> Create(EmployeeProject fields);
        Task<int> Update(int id, EmployeeProject fields);
        Task<int> Delete(int id);
    }
}
