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
                                    ,p.Name as ProjectName
	                                ,ReceivedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,Format(PaymentDate, 'dd/MM/yyyy') as PaymentDate
                                FROM ProjectPayments pp
                                Inner Join Projects p on p.ProjectId = pp.ProjectId
                                WHERE pp.IsDeleted = 0 and p.IsDeleted = 0 
                                Order by pp.CreatedDate desc";

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
                                    ,ProjectId
	                                ,ReceivedAmount
                                    ,BalancedAmount
                                    ,Concat_Ws('/',PaymentMonth,PaymentYear) as PaymentMonthYear
                                    ,Format(PaymentDate, 'dd/MM/yyyy') AS PaymentDate
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
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(ProjectPayment fields)
        {
            try
            {
                var query = @"INSERT INTO ProjectPayments(ProjectId, ReceivedAmount, PaymentMonth, PaymentYear, BalancedAmount,
                              PaymentDate, CreatedBy, CreatedDate) 
                              VALUES (@ProjectId, @ReceivedAmount, 2, 2022, @BalancedAmount, @PaymentDate, @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,
                        //fields.PaymentMonthYear,
                        fields.PaymentDate,
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

        public Task<bool> Update(int id, ProjectPayment fields)
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
	                                ,ModifiedBy = @ManagedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.ProjectId,
                        fields.ReceivedAmount,
                        fields.BalancedAmount,
                        //fields.PaymentMonthYear,
                        fields.PaymentDate,
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
                var query = @"UPDATE ProjectPayments
                                SET  IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE ProjectPaymentId = @id";

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
