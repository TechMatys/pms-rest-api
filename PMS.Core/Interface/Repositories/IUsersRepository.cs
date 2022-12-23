using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UsersListModel>> GetAllUsersAsync();
        Task<Users> GetUsersByIdAsync(int id);
        Task<int?> CreateAsync(Users fields);
        Task<int?> UpdateAsync(int id, Users fields);
        Task<int?> DeleteAsync(int id);
    }
}
