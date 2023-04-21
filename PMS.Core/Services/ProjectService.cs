using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class ProjectService: IProjectService
    {
        public readonly IProjectRepository _ProjectRepository;

        public ProjectService(IProjectRepository ProjectRepository)
        {
            _ProjectRepository = ProjectRepository ?? throw new ArgumentNullException(nameof(ProjectRepository));
        }

        public async Task<IEnumerable<ProjectListModel>> GetAllProject()
        {
            return await _ProjectRepository.GetAllProjectAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _ProjectRepository.GetProjectByIdAsync(id);
        }

        public async Task<int?> Create(Project fields)
        {
            return await _ProjectRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, Project fields)
        {
            return await _ProjectRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _ProjectRepository.DeleteAsync(id);
        }
    }
}
