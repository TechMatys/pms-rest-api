using PMS.Core.Model;


namespace PMS.Core.Interface.Services
{
    public interface IEmployeeProjectService
    {
        Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProject();
        Task<EmployeeProject> GetEmployeeProjectById(int id);
        Task<bool> Create(EmployeeProject fields);
        Task<bool> Update(int id, EmployeeProject fields);
        Task<bool> Delete(int id);

    }
}