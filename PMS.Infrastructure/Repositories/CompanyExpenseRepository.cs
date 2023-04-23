using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class CompanyExpenseRepository : ICompanyExpenseRepository
    {
        private readonly IConfiguration configuration;

        public CompanyExpenseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpensesAsync()
        {
            try
            {
                var query = @"SELECT CompanyExpenseId as Id
	                                ,Title
	                                ,Amount
	                                ,CONCAT_WS(space(1), emp.FirstName, emp.LastName) AS PaidBy
	                                ,Format(ExpenseDate, 'dd/MM/yyyy') as ExpenseDate
                                FROM CompanyExpenses ce
								Left Join Employees emp on emp.EmployeeId = ce.PaidBy
                                WHERE ce.IsDeleted = 0
                                ORDER BY convert(date,ExpenseDate) DESC, ce.CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<CompanyExpenseListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<CompanyExpense> GetCompanyExpenseByIdAsync(int id)
        {
            try
            {
                var query = @"SELECT CompanyExpenseId as Id
	                                ,Title
	                                ,Amount
	                                ,Format(ExpenseDate, 'MM/dd/yyyy') AS ExpenseDate
	                                ,PaidBy
                                    ,Notes
                                 FROM CompanyExpenses
                                WHERE CompanyExpenseId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<CompanyExpense>(query, new
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

        public async Task<int?> CreateAsync(CompanyExpense fields)
        {
            try
            {
                var query = @"INSERT INTO CompanyExpenses(Title, Amount, PaidBy, ExpenseDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@Title, @Amount, @PaidBy, @ExpenseDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {
                        fields.Title,
                        fields.Amount,
                        fields.ExpenseDate,
                        fields.Notes,
                        fields.PaidBy,
                        fields.ManagedBy,
                    });

                    return result;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> UpdateAsync(int id, CompanyExpense fields)
        {
            try
            {
                var query = @"UPDATE CompanyExpenses
                                SET Title = @Title
                                    ,Amount = @Amount
                                    ,ExpenseDate = @ExpenseDate
                                    ,PaidBy = @PaidBy
                                    ,Notes = @Notes                                   
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE CompanyExpenseId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {

                        fields.Title,
                        fields.Amount,
                        fields.ExpenseDate,
                        fields.PaidBy,
                        fields.Notes,
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
                var query = @"UPDATE CompanyExpenses
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE CompanyExpenseId = @id";

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
