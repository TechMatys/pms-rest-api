using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IProjectPaymentRepository
    {
        Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPaymentAsync();
        Task<ProjectPayment> GetProjectPaymentByIdAsync(int id);
        Task<int?> CreateAsync(ProjectPayment fields);
        Task<int?> UpdateAsync(int id, ProjectPayment fields);
        Task<int?> DeleteAsync(int id);
    }
}
