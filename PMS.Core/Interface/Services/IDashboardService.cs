using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IDashboardService
    {
        Task<DashboardModal> GetDashboardItems();
    }
}
