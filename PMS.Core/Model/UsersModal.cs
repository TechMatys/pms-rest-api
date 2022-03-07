

namespace PMS.Core.Model
{
    public class UsersListModel
    {
        public int UserId { get; set; }
        public string? EmployeeName { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
     
        public string? CreatedDate { get; set; }
      
    }
    public class Users
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public int? ScreenPermissionId { get; set; }
        public int? StatusId { get; set; }
        public string? Password { get; set; }
      
        public int? ManagedBy { get; set; }
    }
}
