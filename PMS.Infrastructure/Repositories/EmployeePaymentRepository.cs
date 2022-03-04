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
                                    ,PaymentDate
                                FROM EmployeePayments ep
                                Inner Join Employees e on e.EmployeeId = ep.EmployeeId
                                WHERE ep.IsDeleted = 0 and e.IsDeleted = 0";

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
                var query = @"SELECT EmployeeId
                                    ,Amount
	                                ,PaymentMonth
                                    ,PaymentYear
                                    ,PaymentDate
                                    ,Notes
                                    
                              FROM EmployeePayments where EmployeePaymentId = @EmployeePaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<EmployeePayment>(query, new
                    {
                        EmployeePaymentId = id

                    })).FirstOrDefault();
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
                var query = @"INSERT INTO EmployeePayments(EmployeeId, Amount, PaymentMonth, PaymentYear, PaymentDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@EmployeeId, @Amount, @PaymentMonth, @PaymentYear, @PaymentDate,  @Notes,  -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonth,
                        fields.PaymentYear,
                        fields.PaymentDate,
                        fields.Notes,
                        
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
	                                ,PaymentMonth = @PaymentMonth
                                    ,PaymentYear = @PaymentYear
                                    ,PaymentDate =@PaymentDate
                                    ,Notes = @Notes
                                   
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE EmployeePaymentId = @EmployeePaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.EmployeeId,
                        fields.Amount,
                        fields.PaymentMonth,
                        fields.PaymentYear,
                        fields.PaymentDate,
                        fields.Notes,

                        EmployeePaymentId = id
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
                                WHERE EmployeePaymentId = @EmployeePaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        EmployeePaymentId = id
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
