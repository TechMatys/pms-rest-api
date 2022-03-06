using Dapper;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PMS.Infrastructure.Repositories
{
    public class GlobalCodeRepository : IGlobalCodeRepository
    {
        private readonly IConfiguration configuration;

        public GlobalCodeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<GlobalCodes>> GetAllStates()
        {
            try
            {
                var query = @"SELECT StateId AS Id
	                                ,Name AS Name
                                FROM States
                                WHERE IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<GlobalCodes>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<IEnumerable<GlobalCodes>> GetAllGlobalCodes(string category)
        {
            try
            {
                var query = @"SELECT GlobalCodeId AS Id
	                                ,CodeName AS Name
                                FROM GlobalCodes
                                WHERE IsDeleted = 0 AND Category = @Category";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<GlobalCodes>(query, new
                    {
                        Category = category
                    })).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
