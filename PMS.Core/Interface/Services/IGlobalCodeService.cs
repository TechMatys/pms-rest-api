using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IGlobalCodeService
    {
        Task<IEnumerable<GlobalCodes>> GetAllStates();

    }
}
