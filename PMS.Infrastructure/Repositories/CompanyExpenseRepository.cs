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

        public async Task<IEnumerable<CompanyExpenseListModel>> GetAllCompanyExpenses()
        {
            try
            {
                var query = @"SELECT CompanyExpenseId
	                                ,Title
	                                ,Amount
	                                ,'' AS Createdby
	                                ,ExpenseDate
                                    ,CreatedDate
                                FROM CompanyExpenses
                                WHERE IsDeleted = 0
                                ORDER BY CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<CompanyExpenseListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<CompanyExpense> GetCompanyExpenseById(int id)
        {
            try
            {
                var query = @"SELECT CompanyExpenseId
	                                ,Title
	                                ,Amount
	                                ,Format(ExpenseDate, 'dd/MM/yyyy') AS ExpenseDate
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
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<int> Create(CompanyExpense fields)
        {
            try
            {
                var query = @"INSERT INTO CompanyExpenses(Title, Amount, ExpenseDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@Title, @Amount, @ExpenseDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {
                        fields.Title,
                        fields.Amount,
                        fields.ExpenseDate,
                        fields.Notes,
                        fields.ManagedBy,
                    });

                    return Task.FromResult(result);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> Update(int id, CompanyExpense fields)
        {
            try
            {
                var query = @"UPDATE CompanyExpenses
                                SET Title = @Title
                                    ,Amount = @Amount
                                    ,ExpenseDate = @ExpenseDate
                                    ,Notes = @Notes                                   
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE CompanyExpenseId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {

                        fields.Title,
                        fields.Amount,
                        fields.ExpenseDate,
                        fields.Notes,
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
                var query = @"UPDATE CompanyExpenses
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE CompanyExpenseId = @id";

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
