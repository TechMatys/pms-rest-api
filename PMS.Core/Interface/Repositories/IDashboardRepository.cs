using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardModal> GetDashboardItems();
    }
}
