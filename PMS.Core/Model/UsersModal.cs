

namespace PMS.Core.Model
{
    public class UsersListModel
    {
        public int UserId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }     
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class Users
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public int? ScreenPermissionId { get; set; }
        public int? StatusId { get; set; }      
        public int? ManagedBy { get; set; }
    }

    public class UserLoggedInDetail
    {
        public DateTime LoggedDate { get; set; }
        public string? IpAddress { get; set; }
        public string? SystemName { get; set; }
        public int? ManagedBy { get; set; }
    }
}
