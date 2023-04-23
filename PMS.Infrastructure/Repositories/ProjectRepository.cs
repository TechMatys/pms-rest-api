using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories

{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration configuration;

        public ProjectRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<ProjectListModel>> GetAllProjectAsync()
        {
            try
            {
                    var query = @"SELECT ProjectId as Id
	                                    ,Name
	                                    ,Technologies
	                                    ,gc.CodeName AS STATUS
	                                    ,StartDate
	                                    ,'' AS CreatedBy
	                                    ,p.CreatedDate as CreatedDate
                                    FROM Projects p
                                    LEFT JOIN GlobalCodes gc ON gc.GlobalCodeId = p.StatusId
                                    WHERE p.IsDeleted = 0
                                    ORDER BY p.CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<ProjectListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            try
            {
                var query = @"SELECT ProjectId as Id
	                                ,Name
	                                ,OwnerName
	                                ,Description
	                                ,Technologies
	                                ,DurationId
	                                ,StatusId
	                                ,Format(StartDate, 'dd/MM/yyyy') AS StartDate
	                                ,Format(CompletionDate, 'dd/MM/yyyy') AS CompletionDate
	                                ,BudgetAmount
                                FROM projects
                                WHERE ProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<Project>(query, new
                    {
                        id
                    }));
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> CreateAsync(Project fields)
        {
            try
            {
                var query = @"IF NOT EXISTS(SELECT TOP 1 ProjectId
                                            FROM Projects
                                            WHERE Name = @Name AND IsDeleted = 0) 
                              BEGIN 
                                     INSERT INTO Projects(Name, OwnerName, Description, Technologies, DurationId, StatusId, StartDate, CompletionDate,
                                     BudgetAmount, CreatedBy, CreatedDate) 
                                     VALUES (@Name, @OwnerName, @Description, @Technologies, @DurationId, @StatusId, @StartDate, @CompletionDate,
                                     @BudgetAmount,@ManagedBy, GetUtcDate()) 
                              END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.Name,
                        fields.OwnerName,
                        fields.Description,
                        fields.Technologies,
                        fields.DurationId,
                        fields.StatusId,
                        fields.StartDate,
                        fields.CompletionDate,
                        fields.BudgetAmount,
                        fields.ManagedBy,
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> UpdateAsync(int id, Project fields)
        {
            try
            {
                var query = @"IF NOT EXISTS(SELECT TOP 1 ProjectId
                                            FROM Projects
                                            WHERE Name = @Name AND IsDeleted = 0 and ProjectId <> @id) 
                              BEGIN 
                                     UPDATE Projects
                                        SET Name = @Name
                                            ,OwnerName = @OwnerName
                                            ,Description = @Description
                                            ,Technologies = @Technologies
                                            ,DurationId = @DurationId
                                            ,StatusId = @StatusId
                                            ,StartDate = @StartDate
                                            ,CompletionDate = @CompletionDate
                                            ,BudgetAmount = @BudgetAmount                                    
	                                        ,ModifiedBy = @ManagedBy
	                                        ,ModifiedDate = GetUtcDate()
                                        WHERE ProjectId = @id 
                            END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.Name,
                        fields.OwnerName,
                        fields.Description,
                        fields.Technologies,
                        fields.DurationId,
                        fields.StatusId,
                        fields.StartDate,
                        fields.CompletionDate,
                        fields.BudgetAmount,
                        fields.ManagedBy,
                        id
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> DeleteAsync(int id)
        {
            try
            {
                var query = @"UPDATE Projects
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        id
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }
    }
}
