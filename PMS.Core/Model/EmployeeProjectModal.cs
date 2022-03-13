
namespace PMS.Core.Model
{
    public class EmployeeProjectListModel
    {


        public int EmployeeProjectId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? AssignedDate{ get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

    }

    public class EmployeeProject
    {
        public int EmployeeProjectId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string? AssignedDate { get; set; }        
        public string? Notes { get; set; }
        public int? ManagedBy { get; set; }

    }
}
