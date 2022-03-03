using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeListModel>> GetAllEmployee()
        {
            try
            {
                var query = @"SELECT EmployeeId
	                                ,FirstName
	                                ,LastName
                                    ,EmailAddress
	                                ,Convert(VARCHAR(10), StartDate, 110) AS StartDate
	                                ,gc.CodeName AS Designation
	                                ,'' AS CreatedBy
	                                ,Convert(VARCHAR(10), emp.CreatedDate, 110) AS CreatedDate
                                FROM Employees emp
                                LEFT JOIN GlobalCodes gc ON gc.GlobalCodeId = emp.DesignationId
                                WHERE emp.IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeeListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var query = @"SELECT EmployeeId
                                    ,FirstName
                                    ,MiddleName
	                                ,LastName
                                    ,Gender
                                    ,DateOfBirth
                                    ,EmailAddress
                                    ,Mobile
                                    ,Address
                                    ,City
                                    ,StateId
                                    ,PostalCode
                                    ,StartDate
                                    ,EndDate
                              FROM Employees where EmployeeId = @EmployeeId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<Employee>(query, new
                    {
                        EmployeeId = id

                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(Employee fields)
        {
            try
            {
                var query = @"INSERT INTO Employees(FirstName, MiddleName, LastName, Gender, DateOfBirth, EmailAddress, Mobile, Address, City, StateId, PostalCode, StartDate, EndDate, CreatedBy, CreatedDate) 
                              VALUES (@FirstName, @MiddleName, @LastName, @Gender, @DateOfBirth,  @EmailAddress, @Mobile, @Address, @City, @StateId, @PostalCode, @StartDate, @EndDate, -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.FirstName,
                        fields.MiddleName,
                        fields.LastName,
                        fields.Gender,
                        fields.DateOfBirth,
                        fields.EmailAddress,
                        fields.Mobile,
                        fields.Address,
                        fields.City,
                        fields.StateId,
                        fields.PostalCode,
                        fields.StartDate,
                        fields.EndDate,
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, Employee fields)
        {
            try
            {
                var query = @"UPDATE Employees
                                SET FirstName = @FirstName
                                    ,MiddleName = @MiddleName
	                                ,LastName = @LastName
                                    ,Gender = @Gender
                                    ,DateOfBirth =@DateOfBirth
                                    ,EmailAddress = @EmailAddress
                                    ,Mobile = @Mobile
                                    ,Address =@Address
                                    ,City =@City
                                    ,StateId =@StateId
                                    ,PostalCode = @PostalCode
                                    ,StartDate= @StartDate
                                    ,EndDate= @EndDate
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE EmployeeId = @EmployeeId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.FirstName,
                        fields.MiddleName,
                        fields.LastName,
                        fields.Gender,
                        fields.DateOfBirth,
                        fields.EmailAddress,
                        fields.Mobile,
                        fields.Address,
                        fields.City,
                        fields.StateId,
                        fields.PostalCode,
                        fields.StartDate,
                        fields.EndDate,
                    
                    EmployeeId = id
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
                var query = @"UPDATE Employees
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeeId = @EmployeeId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        EmployeeId = id
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
