using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IAccountService
    {
      
        Task<Account> GetUserById(int id);
      
        Task<bool> Update(int id, Account fields);
     
    }
}
