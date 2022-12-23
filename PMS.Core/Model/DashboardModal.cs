
namespace PMS.Core.Model
{
    public class DashboardModal
    {
        public long TotalEmployees { get; set; }
        public long TotalProjects { get; set; }
        public long MonthlyEarning { get; set; }
        public long AnnualEarning { get; set; }
        public long RemaningEarnings { get; set; }
        public List<ProjectRevenue?> ProjectRevenue { get; set; }
        public List<ProjectStatusDetail?> ProjectStatusDetail { get; set; }
    }

    public class ProjectRevenue
    {
        public string Month { get; set; }
        public long Amount { get; set; }
    }

    public class ProjectStatusDetail
    {
        public long TotalProject { get; set; }
        public string Status { get; set; }
        public string Percentage { get; set; }
    }

}
