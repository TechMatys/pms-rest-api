using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeeProjectRepository
    {
        Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProject();
        Task<EmployeeProject> GetEmployeeProjectById(int id);
        Task<bool> Create(EmployeeProject fields);
        Task<bool> Update(int id, EmployeeProject fields);
        Task<bool> Delete(int id);
    }
}
