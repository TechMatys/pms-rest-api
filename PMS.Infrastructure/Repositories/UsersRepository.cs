using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories

{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration configuration;

        public UsersRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<UsersListModel>> GetAllUsers()
        {
            try
            {
                var query = @"SELECT UserId
	                                    ,EmployeeName
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
                    return (await connection.QueryAsync<UsersListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Users> GetUsersById(int id)
        {
            try
            {
                var query = @"SELECT UserId
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
                    return (await connection.QueryFirstOrDefaultAsync<Users>(query, new
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

        public Task<bool> Create(Users fields)
        {
            try
            {
                var query = @"INSERT INTO Users(Name, OwnerName, Description, Technologies, DurationId, StatusId, StartDate, CompletionDate,
                                     BudgetAmount, CreatedBy, CreatedDate) 
                              VALUES (@Name, @OwnerName, @Description, @Technologies, @DurationId, @StatusId, @StartDate, @CompletionDate,
                                     @BudgetAmount,@ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        
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

        public Task<bool> Update(int id, Users fields)
        {
            try
            {
                var query = @"UPDATE Users
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
                var query = @"UPDATE Userss
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE UsersId = @id";

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
