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
                var query = @"DECLARE @TotalEmployees bigint = 0
	                                ,@TotalProjects bigint = 0
	                                ,@MonthlyEarning bigint = 0
	                                ,@AnnualEarning bigint = 0
                                    ,@TotalCompanyExpenses bigint = 0 
                                    ,@TotalEmployeePayment bigint = 0 
                                    ,@TotalProjectPayment bigint = 0

                                SELECT @TotalEmployees = Count(EmployeeId) FROM Employees 
                                WHERE IsDeleted = 0

                                SELECT @TotalProjects = Count(ProjectId) FROM Projects 
                                WHERE IsDeleted = 0

                                Select @TotalCompanyExpenses = Sum(Amount) from CompanyExpenses where IsDeleted = 0
                                Select @TotalEmployeePayment = Sum(Amount) from EmployeePayments where IsDeleted = 0
                                Select @TotalProjectPayment = Sum(ReceivedAmount) from ProjectPayments where IsDeleted = 0

                                SELECT @MonthlyEarning = Sum(ReceivedAmount) FROM ProjectPayments 
                                WHERE IsDeleted = 0 AND PaymentMonth = Month(GetUtcDate()) AND PaymentYear = Year(GetUtcDate())

                                SELECT @AnnualEarning = Sum(ReceivedAmount) FROM ProjectPayments
                                WHERE IsDeleted = 0 And PaymentYear = Year(GetUtcDate())

                                SELECT @TotalEmployees AS TotalEmployees
                                    ,@TotalProjects as TotalProjects
	                                ,@TotalProjectPayment - (@TotalCompanyExpenses + @TotalEmployeePayment) AS RemaningEarnings
	                                ,@MonthlyEarning AS MonthlyEarning
	                                ,@AnnualEarning AS AnnualEarning

                                SELECT gc.CodeName AS Status, Count(p.ProjectId) AS TotalProject, 
                                Case When @TotalProjects = 0 then '0%' else Concat((Count(p.ProjectId)*100)/@TotalProjects,'%') end as Percentage
                                FROM GlobalCodes gc
                                LEFT JOIN Projects p ON gc.GlobalCodeId = p.StatusId AND p.IsDeleted = 0
                                WHERE gc.Category = 'ProjectStatus'
                                GROUP BY CodeName

                                SELECT Sum(ReceivedAmount) AS Amount, DATENAME(Month,PaymentMonth) AS [Month]
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
