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
            return await _ProjectRepository.GetAllProject();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _ProjectRepository.GetProjectById(id);
        }

        public async Task<int> Create(Project fields)
        {
            return await _ProjectRepository.Create(fields);
        }

        public async Task<int> Update(int id, Project fields)
        {
            return await _ProjectRepository.Update(id, fields);
        }

        public async Task<int> Delete(int id)
        {
            return await _ProjectRepository.Delete(id);
        }
    }
}
