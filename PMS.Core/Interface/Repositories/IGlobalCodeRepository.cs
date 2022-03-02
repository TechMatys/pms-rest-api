using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IGlobalCodeRepository
    {
        Task<IEnumerable<GlobalCodes>> GetAllStates();
        Task<IEnumerable<GlobalCodes>> GetAllGlobalCodes(string category);
    }
}
