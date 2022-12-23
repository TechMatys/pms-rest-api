using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersListModel>> GetAllUsers();
        Task<Users> GetUsersById(int id);
        Task<int?> Create(Users fields);
        Task<int?> Update(int id, Users fields);
        Task<int?> Delete(int id);
    }
}
