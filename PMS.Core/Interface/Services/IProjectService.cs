using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListModel>> GetAllProject();
        Task<Project> GetProjectById(int id);
        Task<int?> Create(Project fields);
        Task<int?> Update(int id, Project fields);
        Task<int?> Delete(int id);
    }
}
