using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text.Json;

namespace PMS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeListModel>> GetAllEmployeeAsync()
        {
            try
            {
                var query = @"SELECT EmployeeId as Id
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
                return null;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var query = @"SELECT EmployeeId as Id
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
                return null;
            }
        }

        public async Task<int?> CreateAsync(Employee fields)
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
                    var result = await connection.ExecuteAsync(query, new
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

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> UpdateAsync(int id, Employee fields)
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
                    var result = await connection.ExecuteAsync(query, new
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
                var query = @"UPDATE Employees
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeeId = @id";

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

        public async Task<IEnumerable<EmployeeTaskListModel>> GetAllTaskDetailsAsync(int id)
        {
            try
            {
                var query = @"SELECT EmployeeTaskDetailId
	                              ,Concat_WS(space(1), FirstName, LastName) AS EmployeeName
	                              ,TaskDate
	                              ,gc.CodeName AS STATUS
	                              ,task.CreatedDate AS CreatedDate
                              FROM EmployeeTaskDetails task
                              INNER JOIN Employees emp ON task.EmployeeId = emp.EmployeeId
                              LEFT JOIN GlobalCodes gc ON gc.GlobalCodeId = task.StatusId And gc.Category = 'TaskStatus'
                              WHERE task.IsDeleted = 0
	                              AND emp.IsDeleted = 0
	                              AND (
		                              @employeeId = 0
		                              OR task.EmployeeId = @employeeId
		                              )
                              ORDER BY CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeeTaskListModel>(query, new
                    {
                        employeeId = id
                    })).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<EmployeeTaskDetails> GetTaskDetailByIdAsync(int id, int taskId)
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
                    var employeeTask = await connection.QueryFirstOrDefaultAsync<EmployeeTaskDetails>(query, new
                    {
                        id
                    });
                    return employeeTask;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> CreateTaskAsync(int id, EmployeeTaskDetails fields)
        {
            try
            {
                string subtaskQuery = string.Empty;
                int loop = 1;
                if (fields.SubtaskDetails != null && fields.SubtaskDetails.Count > 0)
                {
                    fields.SubtaskDetails.ForEach(item =>
                    {
                        subtaskQuery = subtaskQuery + @"Insert into EmployeeSubTaskDetails(EmployeeTaskDetailId, Title, TaskNo, 
                        StatusId, CreatedBy, CreatedDate) values(@Id, '"+ item.Title + "', " + loop + ", " + item.StatusId + ", @ManagedBy, GetUtcDate()) ";

                        loop = loop + 1;
                    });
                }

                var query = @"Declare @Id int = 0; 
                              IF NOT EXISTS (
		                              SELECT TOP 1 EmployeeId
		                              FROM EmployeeTaskDetails
		                              WHERE Convert(DATE, TaskDate) = Convert(DATE, @TaskDate)
			                              AND EmployeeId = @EmployeeId
			                              AND IsDeleted = 0
		                              )
                              BEGIN
	                              INSERT INTO EmployeeTaskDetails (
		                              EmployeeId
		                              ,TaskDate
		                              ,StatusId
                                      ,Note
		                              ,CreatedBy
		                              ,CreatedDate
		                              )
	                              VALUES (
		                              @EmployeeId
		                              ,@TaskDate
		                              ,@StatusId
                                      ,@Note
		                              ,@ManagedBy
		                              ,GetUtcDate()
		                              )
                              END
                              Set @Id = SCOPE_IDENTITY() ";

                query = query + subtaskQuery;

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        EmployeeId = id,
                        fields.TaskDate,
                        fields.Subject,
                        fields.Note,
                        fields.StatusId,
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

        public async Task<int?> DeleteTaskAsync(int id, int taskId)
        {
            try
            {
                var query = @"UPDATE EmployeeTaskDetails
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeeTaskDetailId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        id = taskId
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
