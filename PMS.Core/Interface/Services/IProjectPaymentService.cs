using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IProjectPaymentService
    {
        Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment();
        Task<ProjectPayment> GetProjectPaymentById(int id);
        Task<int?> Create(ProjectPayment fields);
        Task<int?> Update(int id, ProjectPayment fields);
        Task<int?> Delete(int id);
    }
}
