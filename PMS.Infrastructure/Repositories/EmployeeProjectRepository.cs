using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly IConfiguration configuration;

        public EmployeeProjectRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeProjectListModel>> GetAllEmployeeProject()
        {
            try
            {
                var query = @"SELECT EmployeeProjectId
	                                ,Concat_Ws(' ', FirstName, LastName) AS EmployeeName
	                                ,Name AS ProjectName
	                                ,Format(AssignedDate, 'dd/MM/yyyy') AS AssignedDate
	                                ,'' AS CreatedBy
                                FROM EmployeeProjects ep
                                INNER JOIN Employees e ON e.EmployeeId = ep.EmployeeId
                                INNER JOIN Projects p ON p.ProjectId = ep.ProjectId
                                WHERE ep.IsDeleted = 0 AND e.IsDeleted = 0
                                ORDER BY ep.CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeeProjectListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<EmployeeProject> GetEmployeeProjectById(int id)
        {
            try
            {

                var query = @"SELECT EmployeeProjectId
	                                ,EmployeeId
	                                ,ProjectId
	                                ,Format(AssignedDate, 'dd/MM/yyyy') AS AssignedDate
	                                ,Notes
                                FROM EmployeeProjects
                                WHERE EmployeeProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<EmployeeProject>(query, new
                    {
                        id
                    }));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(EmployeeProject fields)
        {
            try
            {

                var query = @"INSERT INTO EmployeePayments(EmployeeId, ProjectId, AssignedDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@EmployeeId, @ProjectId, @AssignedDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.ProjectId,                      
                        fields.AssignedDate,
                        fields.Notes,
                        fields.ManagedBy
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, EmployeeProject fields)
        {
            try
            {
                var query = @"UPDATE EmployeeProjects
                              SET EmployeeId = @EmployeeId
	                              ,ProjectId = @ProjectId
	                              ,AssignedDate = @AssignedDate
	                              ,Notes = @Notes
	                              ,ModifiedBy = @ManagedBy
	                              ,ModifiedDate = GetUtcDate()
                              WHERE EmployeeProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.ProjectId,                      
                        fields.AssignedDate,
                        fields.Notes,
                        fields.ManagedBy,
                        id
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Delete(int id)
        {
            try
            {
                var query = @"UPDATE EmployeeProjects
                              SET IsDeleted = 1
	                              ,DeletedBy = - 1
	                              ,DeletedDate = GetUtcDate()
                              WHERE EmployeeProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        id
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }
    }
}
