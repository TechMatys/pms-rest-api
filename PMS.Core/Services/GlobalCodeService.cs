using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class GlobalCodeService : IGlobalCodeService
    {
        public readonly IGlobalCodeRepository _GlobalCodeRepository;

        public GlobalCodeService(IGlobalCodeRepository GlobalCodeRepository)
        {
            _GlobalCodeRepository = GlobalCodeRepository ?? throw new ArgumentNullException(nameof(GlobalCodeRepository));
        }

        public async Task<IEnumerable<GlobalCodes>> GetAllStates()
        {
            return await _GlobalCodeRepository.GetAllStates();
        }
    }
}
