using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class CompanyInvoiceRepository : ICompanyInvoiceRepository
    {
        private readonly IConfiguration configuration;

        public CompanyInvoiceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoices()
        {
            try
            {
                var query = @"SELECT CompanyInvoiceId
	                                ,Title
	                                ,Createdby
	                                ,Format(GeneratedDate, 'dd/MM/yyyy') AS GeneratedDate
                                FROM CompanyInvoices
                                WHERE IsDeleted = 0
                                ORDER BY CreatedDate DESC";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<CompanyInvoiceListModel>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<CompanyInvoice> GetCompanyInvoiceById(int id)
        {
            try
            {
                var query = @"SELECT CompanyInvoiceId
	                                ,Title
                              FROM CompanyInvoices
                              WHERE CompanyInvoiceId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<CompanyInvoice>(query, new
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

        public Task<int> Create(CompanyInvoice fields)
        {
            try
            {
                var query = @"INSERT INTO CompanyInvoices(Title, GeneratedDate, CreatedBy, CreatedDate) 
                              VALUES (@Title, GetUtcDate(), @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = connection.Execute(query, new
                    {
                        fields.Title,
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
                
        public Task<int> Delete(int id)
        {
            try
            {
                var query = @"UPDATE CompanyInvoices
                                 SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE CompanyInvoiceId = @id";

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
