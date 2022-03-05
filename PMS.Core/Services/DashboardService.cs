using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class DashboardService : IDashboardService
    {
        public readonly IDashboardRepository _DashboardRepository;

        public DashboardService(IDashboardRepository DashboardRepository)
        {
            _DashboardRepository = DashboardRepository ?? throw new ArgumentNullException(nameof(DashboardRepository));
        }

        public async Task<DashboardModal> GetDashboardItems()
        {
            return await _DashboardRepository.GetDashboardItems();
        }
    }
}
