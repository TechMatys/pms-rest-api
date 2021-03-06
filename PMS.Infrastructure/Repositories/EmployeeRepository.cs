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
	                                ,StartDate
	                                ,gc.CodeName AS Designation
	                                ,'' AS CreatedBy
	                                ,emp.CreatedDate AS CreatedDate
                                FROM Employees emp
                                LEFT JOIN GlobalCodes gc ON gc.GlobalCodeId = emp.DesignationId
                                WHERE emp.IsDeleted = 0 
                                Order by CreatedDate desc";

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
                                    ,Format(DateOfBirth, 'dd/MM/yyyy') as DateOfBirth
                                    ,EmailAddress
                                    ,Mobile
                                    ,Address
                                    ,City
                                    ,StateId
                                    ,DesignationId
                                    ,StatusId
                                    ,PostalCode
                                    ,Format(StartDate, 'dd/MM/yyyy') as StartDate
                                    ,Format(EndDate, 'dd/MM/yyyy') as EndDate
                              FROM Employees 
                              Where EmployeeId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, new
                    {
                        id
                    });
                    return employee;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<int> Create(Employee fields)
        {
            try
            {
                var query = @"IF NOT EXISTS(SELECT TOP 1 EmployeeId
                                            FROM Employees
                                            WHERE EmailAddress = @EmailAddress AND IsDeleted = 0) 
                              BEGIN 
                                    INSERT INTO Employees(FirstName, MiddleName, LastName, Gender, DateOfBirth, EmailAddress, Mobile, 
                                    Address, City, StateId, DesignationId, StatusId, PostalCode, StartDate, EndDate, CreatedBy, CreatedDate) 
                                    VALUES (@FirstName, @MiddleName, @LastName, @Gender, @DateOfBirth, @EmailAddress, @Mobile, 
                                    @Address, @City, @StateId, @DesignationId, @StatusId, @PostalCode, @StartDate, @EndDate, @ManagedBy, GetUtcDate()) 
                              END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
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
                        fields.DesignationId,
                        fields.StatusId,
                        fields.PostalCode,
                        fields.StartDate,
                        fields.EndDate,
                        fields.ManagedBy
                    });

                    return Task.FromResult(result);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> Update(int id, Employee fields)
        {
            try
            {
                var query = @"IF NOT EXISTS (SELECT TOP 1 EmployeeId
		                                     FROM Employees
		                                     WHERE EmailAddress = @EmailAddress AND IsDeleted = 0 AND EmployeeId <> @id) 
                              BEGIN 
	                                UPDATE Employees
	                                SET FirstName = @FirstName
		                                ,MiddleName = @MiddleName
		                                ,LastName = @LastName
		                                ,Gender = @Gender
		                                ,DateOfBirth = @DateOfBirth
		                                ,EmailAddress = @EmailAddress
		                                ,Mobile = @Mobile
		                                ,Address = @Address
		                                ,City = @City
		                                ,StateId = @StateId
		                                ,DesignationId = @DesignationId
		                                ,StatusId = @StatusId
		                                ,PostalCode = @PostalCode
		                                ,StartDate = @StartDate
		                                ,EndDate = @EndDate
		                                ,ModifiedBy = @ManagedBy
		                                ,ModifiedDate = GetUtcDate()
	                                WHERE EmployeeId = @id 
                              END";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
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
                        fields.StatusId,
                        fields.DesignationId,
                        fields.PostalCode,
                        fields.StartDate,
                        fields.EndDate,
                        fields.ManagedBy,
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

        public Task<int> Delete(int id)
        {
            try
            {
                var query = @"UPDATE Employees
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeeId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {
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
