using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectListModel>> GetAllProject();
        Task<Project> GetProjectById(int id);
        Task<bool> Create(Project fields);
        Task<bool> Update(int id, Project fields);
        Task<bool> Delete(int id);
    }
}
