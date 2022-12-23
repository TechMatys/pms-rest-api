using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IEmployeeProjectRepository
    {
        Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProjectAsync();
        Task<EmployeeProject> GetEmployeeProjectByIdAsync(int id);
        Task<int?> CreateAsync(EmployeeProject fields);
        Task<int?> UpdateAsync(int id, EmployeeProject fields);
        Task<int?> DeleteAsync(int id);
    }
}
