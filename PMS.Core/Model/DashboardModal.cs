
namespace PMS.Core.Model
{
    public class DashboardModal
    {
        public int TotalEmployees { get; set; }
        public int TotalProjects { get; set; }
        public int MonthlyEarning { get; set; }
        public int AnnualEarning { get; set; }
        public List<ProjectRevenue> ProjectRevenue { get; set; }
        public List<ProjectStatusDetail> ProjectStatusDetail { get; set; }
    }

    public class ProjectRevenue
    {
        public string Month { get; set; }
        public int Amount { get; set; }
    }

    public class ProjectStatusDetail
    {
        public int TotalProject { get; set; }
        public string Status { get; set; }
        public string Percentage { get; set; }
    }

}
