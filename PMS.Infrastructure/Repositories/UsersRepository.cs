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
                var query = @"SELECT USerId
                                    ,CONCAT_WS(' ', emp.FirstName, emp.LastName) AS EmployeeName
	                                ,gc.CodeName AS ROLE
	                                ,gc1.CodeName AS STATUS
	                                ,Format(us.CreatedDate, 'dd/MM/yyyy') AS CreatedDate
                                FROM Users us
                                INNER JOIN Employees emp ON emp.EmployeeId = us.EmployeeId
                                INNER JOIN GlobalCodes gc ON gc.GlobalCodeId = us.RoleId
                                INNER JOIN GlobalCodes gc1 ON gc.GlobalCodeId = us.StatusId
                                WHERE emp.IsDeleted = 0 AND us.IsDeleted = 0
                                ORDER BY us.CreatedDate DESC";

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
	                                ,EmployeeId
	                                ,RoleId
	                                ,StatusId
	                                ,ScreenPermissionId
                                FROM Users
                                WHERE UserId = @id";

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
                var query = @"INSERT INTO Users(EmployeeId, RoleId, StatusId, ScreenPermissionId, CreatedBy, CreatedDate) 
                              VALUES (@EmployeeId, @RoleId, @StatusId, @ScreenPermissionId, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.RoleId,
                        fields.StatusId,
                        fields.ScreenPermissionId,
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

        public Task<bool> Update(int id, Users fields)
        {
            try
            {
                var query = @"UPDATE Users
                                SET EmployeeId = @EmployeeId
                                    ,RoleId = @RoleId
                                    ,StatusId = @StatusId
                                    ,ScreenPermissionId = @ScreenPermissionId                                    
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE ProjectId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.RoleId,
                        fields.StatusId,
                        fields.ScreenPermissionId,
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
                var query = @"UPDATE Users
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
