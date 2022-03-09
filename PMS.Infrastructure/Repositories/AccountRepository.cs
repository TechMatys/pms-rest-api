using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories

{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration configuration;

        public AccountRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
             

        public async Task<Account> GetUserById(int id)
        {
            try
            {
                var query = @"SELECT FirstName
	                                ,LastName
	                                ,EmailAddress
	                                ,Mobile
	                                ,Gender
	                                ,Format(DateOfBirth, 'dd/MM/yyyy') AS DateOfBirth
                                     FROM Users us
                                 INNER JOIN Employees emp ON emp.EmployeeId = us.EmployeeId
                                 WHERE us.UserId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<Account>(query, new
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

        public Task<int> Update(int id, Account fields)
        {
            try
            {
                var query = @"IF NOT EXISTS (SELECT TOP 1 emp.EmployeeId
                                             FROM Employees emp
                                             INNER JOIN Users us ON us.EmployeeId = emp.EmployeeId
                                             WHERE EmailAddress = @EmailAddress
	                                            AND emp.IsDeleted = 0
	                                            AND us.IsDeleted = 0
	                                            AND us.UserId <> @id) 
                              BEGIN 
                                    Update emp set FirstName = @FirstName
	                                            ,LastName = @LastName
	                                            ,EmailAddress = @EmailAddress
	                                            ,Mobile = @Mobile
	                                            ,Gender = @Gender
	                                            ,DateOfBirth = @DateOfBirth
                                                FROM Employees emp
                                    INNER JOIN Users us ON emp.EmployeeId = us.EmployeeId
                                    WHERE us.UserId = @id 

                                    If(@Password != '') 
                                    Begin 
	                                    Update Users set Password = @Password where UserId = @id 
                                    End 
                              END";
                  
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {
                        fields.FirstName,
                        fields.LastName,
                        fields.EmailAddress,
                        fields.Mobile,
                        fields.Gender,
                        fields.DateOfBirth,
                        fields.Password,
                        fields.ConfirmPassword,
                        id
                    });

                    return Task.FromResult(result);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(0);
            }
        }

    }
}
