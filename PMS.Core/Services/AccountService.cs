using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _AccountRepository;

        public AccountService(IAccountRepository AccountRepository)
        {
            _AccountRepository = AccountRepository ?? throw new ArgumentNullException(nameof(AccountRepository));
        }

    

        public async Task<Account> GetUserById(int id)
        {
            return await _AccountRepository.GetUserByIdAsync(id);
        }

        

        public async Task<int?> Update(int id, Account fields)
        {
            return await _AccountRepository.UpdateAsync(id, fields);
        }

        
    }
}
