

namespace PMS.Core.Model
{
    public class ProjectListModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }        
        public DateTime? StartDate { get; set; }
        public string? Technologies { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? StartDate { get; set; }
        public int? DurationId { get; set; }
        public int? StatusId { get; set; }
        public string? CompletionDate { get; set; }
        public string? BudgetAmount { get; set; }
        public int? ManagedBy { get; set; }
    }
}
