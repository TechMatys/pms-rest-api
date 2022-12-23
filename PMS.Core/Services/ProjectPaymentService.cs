using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;


namespace PMS.Core.Services
{
    public class ProjectPaymentService : IProjectPaymentService
    {
        public readonly IProjectPaymentRepository _ProjectPaymentRepository;

        public ProjectPaymentService(IProjectPaymentRepository ProjectPaymentRepository)
        {
            _ProjectPaymentRepository = ProjectPaymentRepository ?? throw new ArgumentNullException(nameof(ProjectPaymentRepository));
        }

        public async Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment()
        {
            return await _ProjectPaymentRepository.GetAllProjectPaymentAsync();
        }

        public async Task<ProjectPayment> GetProjectPaymentById(int id)
        {
            return await _ProjectPaymentRepository.GetProjectPaymentByIdAsync(id);
        }

        public async Task<int?> Create(ProjectPayment fields)
        {
            return await _ProjectPaymentRepository.CreateAsync(fields);
        }

        public async Task<int?> Update(int id, ProjectPayment fields)
        {
            return await _ProjectPaymentRepository.UpdateAsync(id, fields);
        }

        public async Task<int?> Delete(int id)
        {
            return await _ProjectPaymentRepository.DeleteAsync(id);
        }
    
    }
}
