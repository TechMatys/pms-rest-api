using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UsersListModel>> GetAllUsers();
        Task<Users> GetUsersById(int id);
        Task<bool> Create(Users fields);
        Task<bool> Update(int id, Users fields);
        Task<bool> Delete(int id);
    }
}
