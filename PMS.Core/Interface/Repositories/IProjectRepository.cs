using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectListModel>> GetAllProjectAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<int?> CreateAsync(Project fields);
        Task<int?> UpdateAsync(int id, Project fields);
        Task<int?> DeleteAsync(int id);
    }
}
