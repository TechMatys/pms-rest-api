using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersListModel>> GetAllUsers();
        Task<Users> GetUsersById(int id);
        Task<bool> Create(Users fields);
        Task<bool> Update(int id, Users fields);
        Task<bool> Delete(int id);
    }
}
