using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IProjectPaymentRepository
    {
        Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment();
        Task<ProjectPayment> GetProjectPaymentById(int id);
        Task<bool> Create(ProjectPayment fields);
        Task<bool> Update(int id, ProjectPayment fields);
        Task<bool> Delete(int id);
    }
}
