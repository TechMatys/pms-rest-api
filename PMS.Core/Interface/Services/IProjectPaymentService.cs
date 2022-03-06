using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IProjectPaymentService
    {
        Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment();
        Task<ProjectPayment> GetProjectPaymentById(int id);
        Task<bool> Create(ProjectPayment fields);
        Task<bool> Update(int id, ProjectPayment fields);
        Task<bool> Delete(int id);
    }
}
