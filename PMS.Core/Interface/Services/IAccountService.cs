using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IAccountService
    {
      
        Task<Account> GetUserById(int id);
      
        Task<int?> Update(int id, Account fields);
     
    }
}
