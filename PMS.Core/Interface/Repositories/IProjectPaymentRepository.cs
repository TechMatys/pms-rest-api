using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IProjectPaymentRepository
    {
        Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment();
        Task<ProjectPayment> GetProjectPaymentById(int id);
        Task<int> Create(ProjectPayment fields);
        Task<int> Update(int id, ProjectPayment fields);
        Task<int> Delete(int id);
    }
}
