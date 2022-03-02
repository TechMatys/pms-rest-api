
namespace PMS.Core.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public string? DateOfBirth { get; set; }
        public string? EmailAddress { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? StateId { get; set; }

       public string?  PostalCode { get; set; }

       
        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        public string? Designation { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
    }   
}
