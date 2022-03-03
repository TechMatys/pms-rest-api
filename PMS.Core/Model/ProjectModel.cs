

namespace PMS.Core.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? StartDate { get; set; }
        public int? DurationId { get; set; }
        public int? StatusId { get; set; }
        public string? CompletionDate { get; set; }
        public string? BudgetAmount { get; set; }
    }
}
