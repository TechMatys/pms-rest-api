using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class EmployeePaymentRepository : IEmployeePaymentRepository
    {
        private readonly IConfiguration configuration;

        public EmployeePaymentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePaymentAsync()
        {
            try
            {
                var query = @"SELECT EmployeePaymentId
	                                ,Concat_Ws(' ', FirstName, LastName) AS EmployeeName
	                                ,Amount
	                                ,Concat_Ws('/', PaymentMonth, PaymentYear) AS PaymentMonthYear
	                                ,PaymentDate                                    
                                    ,ep.CreatedDate as CreatedDate
                                FROM EmployeePayments ep
                                INNER JOIN Employees e ON e.EmployeeId = ep.EmployeeId
                                WHERE ep.IsDeleted = 0 AND e.IsDeleted = 0
                                ORDER BY PaymentDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeePaymentListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<EmployeePayment> GetEmployeePaymentByIdAsync(int id)
        {
            try
            {
                var query = @"SELECT EmployeePaymentId
	                                ,EmployeeId
	                                ,Amount
	                                ,Concat_Ws('/', PaymentMonth, PaymentYear) AS Month
	                                ,Format(PaymentDate, 'dd/MM/yyyy') AS PaymentDate
	                                ,Notes
                                FROM EmployeePayments
                                WHERE EmployeePaymentId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<EmployeePayment>(query, new
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

        public async Task<int?> CreateAsync(EmployeePayment fields)
        {
            try
            {

                var query = @"INSERT INTO EmployeePayments(EmployeeId, Amount, PaymentDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@EmployeeId, @Amount, @PaymentDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonthYear,
                        fields.PaymentDate,
                        fields.Notes,
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

        public async Task<int?> UpdateAsync(int id, EmployeePayment fields)
        {
            try
            {
                var query = @"UPDATE EmployeePayments
                                SET EmployeeId = @EmployeeId
                                    ,Amount = @Amount
	                               -- ,PaymentMonth = SUBSTRING(@PaymentMonthYear,0,CHARINDEX('/',@PaymentMonthYear,0))
                                    --,PaymentYear = SUBSTRING(@PaymentMonthYear,CHARINDEX('/',@PaymentMonthYear,0)+1,LEN(@PaymentMonthYear))
                                    ,PaymentDate = @PaymentDate
                                    ,Notes = @Notes 
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE EmployeePaymentId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonthYear,
                        fields.PaymentDate,
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
                var query = @"UPDATE EmployeePayments
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeePaymentId = @id";

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
