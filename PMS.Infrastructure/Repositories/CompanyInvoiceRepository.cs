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

        public async Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoicesAsync()
        {
            try
            {
                var query = @"SELECT CompanyInvoiceId
	                                ,Title
	                                ,Createdby
	                                ,GeneratedDate
                                    ,CreatedDate
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
               return null;
            }
        }

        public async Task<CompanyInvoice> GetCompanyInvoiceByIdAsync(int id)
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
                return null;
            }
        }

        public async Task<int?> CreateAsync(CompanyInvoice fields)
        {
            try
            {
                var query = @"INSERT INTO CompanyInvoices(Title, GeneratedDate, CreatedBy, CreatedDate) 
                              VALUES (@Title, GetUtcDate(), @ManagedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result =await connection.ExecuteAsync(query, new
                    {
                        fields.Title,
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
                
        public async Task<int?> DeleteAsync(int id)
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
