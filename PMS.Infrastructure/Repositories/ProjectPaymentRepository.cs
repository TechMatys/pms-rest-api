using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class ProjectPaymentRepository : IProjectPaymentRepository
    {
        private readonly IConfiguration configuration;

        public ProjectPaymentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPaymentAsync()
        {
            try
            {
                var query = @"SELECT ProjectPaymentId
                                    ,p.Name as ProjectName
	                                ,ReceivedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,PaymentDate
                                    ,pp.CreatedDate as CreatedDate
                                FROM ProjectPayments pp
                                Inner Join Projects p on p.ProjectId = pp.ProjectId
                                WHERE pp.IsDeleted = 0 and p.IsDeleted = 0 
                                Order by pp.PaymentDate desc";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<ProjectPaymentListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<ProjectPayment> GetProjectPaymentByIdAsync(int id)
        {
            try
            {
                var query = @"SELECT ProjectPaymentId
                                    ,ProjectId
	                                ,ReceivedAmount
                                    ,BalancedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as Month
                                    ,Format(PaymentDate, 'dd/MM/yyyy') AS PaymentDate
                                    ,Notes
                              FROM ProjectPayments where ProjectPaymentId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<ProjectPayment>(query, new
                    {
                        id

                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<int?> CreateAsync(ProjectPayment fields)
        {
            try
            {
                var query = @"INSERT INTO ProjectPayments(ProjectId, ReceivedAmount, PaymentMonth, PaymentYear, BalancedAmount,
                              PaymentDate, Notes, CreatedBy, CreatedDate) 
                              VALUES (@ProjectId, @ReceivedAmount, 2, 2022, @BalancedAmount, @PaymentDate, @Notes, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,
                        //fields.PaymentMonthYear,
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

        public async Task<int?> UpdateAsync(int id, ProjectPayment fields)
        {
            try
            {
                var query = @"UPDATE ProjectPayments
                                SET ProjectId = @ProjectId
                                    ,ReceivedAmount = @ReceivedAmount
                                    ,BalancedAmount = @BalancedAmount
                                    -- ,PaymentMonth = SUBSTRING(@PaymentMonthYear,0,CHARINDEX('/',@PaymentMonthYear,0))
                                    --,PaymentYear = SUBSTRING(@PaymentMonthYear,CHARINDEX('/',@PaymentMonthYear,0)+1,LEN(@PaymentMonthYear))
                                    ,PaymentDate = @PaymentDate 
                                    ,Notes = @Notes 
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,
                        //fields.PaymentMonthYear,
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
                var query = @"UPDATE ProjectPayments
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @id";

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
