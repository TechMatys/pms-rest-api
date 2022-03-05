
namespace PMS.Core.Model
{
    public class EmployeeProjectListModel
    {


        public int EmployeePaymentId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProjectName { get; set; }
        public string? AssignedDate{ get; set; }
      
        public string? CreatedBy { get; set; }

    }

    public class EmployeeProject
    {

        public int EmployeeId { get; set; }
        public int EmployeeProjectId { get; set; }

        public int? ProjectId { get; set; }

        public string? AssignedDate { get; set; }
        
        public string? Notes { get; set; }
        public int? ManagedBy { get; set; }

    }
}
