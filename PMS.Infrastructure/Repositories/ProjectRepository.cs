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
                                    ,StatusId
                                    ,StartDate
	                                ,Convert(VARCHAR(10), StartDate, 110) AS StartDate	                                
	                                ,'' AS CreatedBy
	                                ,Convert(VARCHAR(10), CreatedDate, 110) AS CreatedDate
                                FROM Projects                                 
                                WHERE IsDeleted = 0";

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
                                    ,StartDate
                                    ,CompletionDate
                                    ,BudgetAmount
                              FROM projects where ProjectId = @ProjectId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<Project>(query, new
                    {
                        ProjectId = id

                    })).FirstOrDefault();
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
                                     @BudgetAmount, -1, GetUtcDate())";

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
                                    ,DurationId    = @DurationId
                                    ,StatusId      = @StatusId
                                    ,StartDate   = @StartDate
                                    ,CompletionDate = @CompletionDate
                                    ,BudgetAmount  = @BudgetAmount
                                    
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE ProjectId = @ProjectId";

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
                        ProjectId = id
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
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectId = @ProjectId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        ProjectId = id
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
