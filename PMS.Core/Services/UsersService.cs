using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class UsersService : IUsersService
    {
        public readonly IUsersRepository _UsersRepository;

        public UsersService(IUsersRepository UsersRepository)
        {
            _UsersRepository = UsersRepository ?? throw new ArgumentNullException(nameof(UsersRepository));
        }

        public async Task<IEnumerable<UsersListModel>> GetAllUsers()
        {
            return await _UsersRepository.GetAllUsers();
        }

        public async Task<Users> GetUsersById(int id)
        {
            return await _UsersRepository.GetUsersById(id);
        }

        public async Task<int> Create(Users fields)
        {
            return await _UsersRepository.Create(fields);
        }

        public async Task<int> Update(int id, Users fields)
        {
            return await _UsersRepository.Update(id, fields);
        }

        public async Task<int> Delete(int id)
        {
            return await _UsersRepository.Delete(id);
        }
    }
}
