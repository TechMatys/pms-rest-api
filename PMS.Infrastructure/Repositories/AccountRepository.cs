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

        public Task<bool> Update(int id, Account fields)
        {
            try
            {
                var query = @"Update emp set FirstName = @FirstName
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
                                End";
                  
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
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
