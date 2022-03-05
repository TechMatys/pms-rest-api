using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration configuration;
        public DashboardRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<DashboardModal> GetDashboardItems()
        {
            try
            {
                DashboardModal objDashboardData = new DashboardModal();
                var query = @"DECLARE @TotalEmployees INT = 0
	                                ,@TotalProjects INT = 0
	                                ,@MonthlyEarning INT = 0
	                                ,@AnnualEarning INT = 0

                                SELECT @TotalEmployees = Count(EmployeeId) FROM Employees 
                                WHERE IsDeleted = 0

                                SELECT @TotalProjects = Count(ProjectId) FROM Projects 
                                WHERE IsDeleted = 0

                                SELECT @MonthlyEarning = Sum(RecievedAmount) FROM ProjectPayments 
                                WHERE IsDeleted = 0 AND PaymentMonth = Month(GetUtcDate()) AND PaymentYear = Year(GetUtcDate())

                                SELECT @AnnualEarning = Sum(RecievedAmount) FROM ProjectPayments
                                WHERE IsDeleted = 0 And PaymentYear = Year(GetUtcDate())

                                SELECT @TotalEmployees AS TotalEmployees
	                                ,@TotalProjects AS TotalProjects
	                                ,@MonthlyEarning AS MonthlyEarning
	                                ,@AnnualEarning AS AnnualEarning

                                SELECT gc.CodeName AS Status, Count(p.ProjectId) AS TotalProject, 
                                Case When @TotalProjects = 0 then '0%' else Concat((Count(p.ProjectId)*100)/@TotalProjects,'%') end as Percentage
                                FROM GlobalCodes gc
                                LEFT JOIN Projects p ON gc.GlobalCodeId = p.StatusId AND p.IsDeleted = 0
                                WHERE gc.Category = 'ProjectStatus'
                                GROUP BY CodeName

                                SELECT Sum(RecievedAmount) AS Amount, DATENAME(Month,PaymentMonth) AS [Month]
                                FROM ProjectPayments
                                WHERE IsDeleted = 0 AND PaymentYear = Year(GetUtcDate())
                                GROUP BY PaymentMonth";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {

                    using (var objDashboardDetail = connection.QueryMultiple(query))
                    {
                        objDashboardData = objDashboardDetail.ReadFirstOrDefault<DashboardModal>();
                        objDashboardData.ProjectStatusDetail = objDashboardDetail.Read<ProjectStatusDetail>().ToList();
                        objDashboardData.ProjectRevenue = objDashboardDetail.Read<ProjectRevenue>().ToList();
                    };

                }
                return objDashboardData;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
