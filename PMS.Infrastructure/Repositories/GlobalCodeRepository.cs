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
                var query = @"SELECT StateId as Id,
                                Name as Name
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
    }
}
