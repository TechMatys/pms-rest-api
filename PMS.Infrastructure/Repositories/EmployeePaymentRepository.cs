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

        public async Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment()
        {
            try
            {
                var query = @"SELECT EmployeePaymentId
	                                ,Concat_Ws(' ',FirstName,LastName) as EmployeeName
                                    ,Amount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,Format(PaymentDate, 'dd/MM/yyyy') as PaymentDate
                                FROM EmployeePayments ep
                                Inner Join Employees e on e.EmployeeId = ep.EmployeeId
                                WHERE ep.IsDeleted = 0 and e.IsDeleted = 0 
                                Order by ep.CreatedDate desc";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeePaymentListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<EmployeePayment> GetEmployeePaymentById(int id)
        {
            try
            {

                var query = @"SELECT EmployeePaymentId
                                    ,EmployeeId
                                    ,Amount
	                                ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,Format(PaymentDate, 'dd/MM/yyyy') AS PaymentDate
                                    ,Notes  
                            FROM EmployeePayments where EmployeePaymentId = @id";

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
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(EmployeePayment fields)
        {
            try
            {

                var query = @"INSERT INTO EmployeePayments(EmployeeId, Amount, PaymentDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@EmployeeId, @Amount, @PaymentDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonthYear,
                        fields.PaymentDate,
                        fields.Notes,
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

        public Task<bool> Update(int id, EmployeePayment fields)
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
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonthYear,
                        fields.PaymentDate,
                        fields.Notes,
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
                var query = @"UPDATE EmployeePayments
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE EmployeePaymentId = @id";

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
