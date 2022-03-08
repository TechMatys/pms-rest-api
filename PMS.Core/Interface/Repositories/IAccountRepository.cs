using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IAccountRepository
    {
       
        Task<Account> GetUserById(int id);
     
        Task<bool> Update(int id, Account fields);
       
    }
}
