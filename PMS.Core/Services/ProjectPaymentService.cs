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
            return await _ProjectPaymentRepository.GetAllProjectPayment();
        }

        public async Task<ProjectPayment> GetProjectPaymentById(int id)
        {
            return await _ProjectPaymentRepository.GetProjectPaymentById(id);
        }

        public async Task<bool> Create(ProjectPayment fields)
        {
            return await _ProjectPaymentRepository.Create(fields);
        }

        public async Task<bool> Update(int id, ProjectPayment fields)
        {
            return await _ProjectPaymentRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _ProjectPaymentRepository.Delete(id);
        }
    
    }
}
