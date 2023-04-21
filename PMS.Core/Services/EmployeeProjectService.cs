using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;


namespace PMS.Core.Services
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        public readonly IEmployeeProjectRepository _EmployeeProjectRepository;

        public EmployeeProjectService(IEmployeeProjectRepository EmployeeProjectRepository)
        {
            _EmployeeProjectRepository = EmployeeProjectRepository ?? throw new ArgumentNullException(nameof(EmployeeProjectRepository));
        }

        public async Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProject()
        {
            return await _EmployeeProjectRepository.GetAllEmployeeProjectAsync();
        }

        public async Task<EmployeeProject> GetEmployeeProjectById(int id)
        {
            return await _EmployeeProjectRepository.GetEmployeeProjectByIdAsync(id);
        }

        public async Task<int?> Create(EmployeeProject fields)
        {
            return await _EmployeeProjectRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, EmployeeProject fields)
        {
            return await _EmployeeProjectRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _EmployeeProjectRepository.DeleteAsync(id);
        }


    }


}
