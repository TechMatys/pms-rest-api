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

        public async Task<IEnumerable<ProjectListModel>> GetAllProject()
        {
            try
            {
                    var query = @"SELECT ProjectId
	                                    ,Name
	                                    ,Technologies
	                                    ,gc.CodeName AS STATUS
	                                    ,Format(StartDate, 'dd/MM/yyyy') AS StartDate
	                                    ,'' AS CreatedBy
	                                    ,Format(p.CreatedDate, 'dd/MM/yyyy') AS CreatedDate
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
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Project> GetProjectById(int id)
        {
            try
            {
                var query = @"SELECT ProjectId
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
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(Project fields)
        {
            try
            {
                var query = @"INSERT INTO Projects(Name, OwnerName, Description, Technologies, DurationId, StatusId, StartDate, CompletionDate,
                                     BudgetAmount, CreatedBy, CreatedDate) 
                              VALUES (@Name, @OwnerName, @Description, @Technologies, @DurationId, @StatusId, @StartDate, @CompletionDate,
                                     @BudgetAmount,@ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
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

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, Project fields)
        {
            try
            {
                var query = @"UPDATE Projects
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
                                WHERE ProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
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
                var query = @"UPDATE Projects
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectId = @id";

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
