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

        public async Task<IEnumerable<UsersListModel>> GetAllUsersAsync()
        {
            try
            {
                var query = @"SELECT UserId
                                    ,CONCAT_WS(' ', emp.FirstName, emp.LastName) AS EmployeeName
	                                ,gc.CodeName AS Role
	                                ,gc1.CodeName AS Status
                                FROM Users us
                                INNER JOIN Employees emp ON emp.EmployeeId = us.EmployeeId
                                INNER JOIN GlobalCodes gc ON gc.GlobalCodeId = us.RoleId
                                INNER JOIN GlobalCodes gc1 ON gc1.GlobalCodeId = us.StatusId
                                WHERE emp.IsDeleted = 0 AND us.IsDeleted = 0
                                ORDER BY us.CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<UsersListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<Users> GetUsersByIdAsync(int id)
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
                return null;
            }
        }

        public async Task<int?> CreateAsync(Users fields)
        {
            try
            {
                var query = @"IF NOT EXISTS(SELECT TOP 1 UserId
                                            FROM Users
                                            WHERE EmployeeId = @EmployeeId AND IsDeleted = 0) 
                              BEGIN 
                                    INSERT INTO Users(EmployeeId, RoleId, StatusId, ScreenPermissionId, CreatedBy, CreatedDate) 
                                    VALUES (@EmployeeId, @RoleId, @StatusId, @ScreenPermissionId, @ManagedBy, GetUtcDate()) 
                              END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.EmployeeId,
                        fields.RoleId,
                        fields.StatusId,
                        fields.ScreenPermissionId,
                        fields.ManagedBy
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> SaveLoggedInDetailAsync(UserLoggedInDetail fields)
        {
            try
            {
                var query = @"INSERT INTO UserLoggedInDetail(LoggedDate, IpAddress, SystemName, CreatedBy, CreatedDate) 
                                    VALUES (@LoggedDate, @IpAddress, @SystemName, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.LoggedDate,
                        fields.IpAddress,
                        fields.SystemName,
                        fields.ManagedBy
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> UpdateAsync(int id, Users fields)
        {
            try
            {
                var query = @"IF NOT EXISTS(SELECT TOP 1 UserId
                                            FROM Users
                                            WHERE EmployeeId = @EmployeeId AND UserId <> @id AND IsDeleted = 0) 
                              BEGIN 
                                    UPDATE Users
                                        SET EmployeeId = @EmployeeId
                                            ,RoleId = @RoleId
                                            ,StatusId = @StatusId
                                            ,ScreenPermissionId = @ScreenPermissionId                                    
	                                        ,ModifiedBy = @ManagedBy
	                                        ,ModifiedDate = GetUtcDate()
                                    WHERE UserId = @id 
                              END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.EmployeeId,
                        fields.RoleId,
                        fields.StatusId,
                        fields.ScreenPermissionId,
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
                var query = @"UPDATE Users
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE UserId = @id";

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
