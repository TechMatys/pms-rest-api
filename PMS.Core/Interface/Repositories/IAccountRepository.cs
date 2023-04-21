using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IAccountRepository
    {
       
        Task<Account> GetUserByIdAsync(int id);
     
        Task<int?> UpdateAsync(int id, Account fields);
       
    }
}
