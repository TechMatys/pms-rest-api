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

        public async Task<IEnumerable<ProjectPaymentListModel>> GetAllProjectPayment()
        {
            try
            {
                var query = @"SELECT ProjectPaymentId
                                    ,Concat_Ws(' ',FirstName) as Name
	                                ,ReceivedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,PaymenttDate
	                                ,Convert(VARCHAR(10), StartDate, 110) AS StartDate	                                
	                                ,'' AS CreatedBy
	                                ,Convert(VARCHAR(10), CreatedDate, 110) AS CreatedDate
                                FROM ProjectPayments                                 
                                WHERE IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<ProjectPaymentListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<ProjectPayment> GetProjectPaymentById(int id)
        {
            try
            {
                var query = @"SELECT ProjectPaymentId
	                                ,ReceivedAmount
                                    ,BalancedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,PaymenttDate
                              FROM projectPayments where ProjectPaymentId = @ProjectPaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<ProjectPayment>(query, new
                    {
                        ProjectPaymentId = id

                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(ProjectPayment fields)
        {
            try
            {
                var query = @"INSERT INTO ProjectPayments(ProjectId, ReceivedAmount, BalancedAmount ,PaymenttDate, CreatedBy, CreatedDate) 
                              VALUES (@ProjectId, @ReceivedAmount, @BalancedAmount, @PaymentDate,  -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,
                        fields.PaymentMonthYear,
                        fields.PaymentDate,
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, ProjectPayment fields)
        {
            try
            {
                var query = @"UPDATE ProjectPayments
                                SET ,ProjectId = @ProjectId
                                    ,ReceivedAmount = @ReceivedAmount
                                    ,BalancedAmount = @BalancedAmount
                                    -- ,PaymentMonth = SUBSTRING(@PaymentMonthYear,0,CHARINDEX('/',@PaymentMonthYear,0))
                                    --,PaymentYear = SUBSTRING(@PaymentMonthYear,CHARINDEX('/',@PaymentMonthYear,0)+1,LEN(@PaymentMonthYear))
                                    ,PaymentDate      = @PaymentDate
                                    
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @ProjectPaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,

                        fields.PaymentMonthYear,

                        fields.PaymentDate,
                        ProjectPaymentId = id
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
                var query = @"UPDATE ProjectPayments
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @ProjectPaymentId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        ProjectPaymentId = id
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
