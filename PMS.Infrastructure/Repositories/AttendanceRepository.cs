using Microsoft.Extensions.Configuration;
using PMS.Core.Interface.Repositories;
using PMS.Core.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IConfiguration configuration;
        public AttendanceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IEnumerable<Attendance>> GetAttendance(int roleId, int id)
        {
            try
            {
                var query = @"SELECT 
                                EmployeeId as Id, 
                                PunchInTime, 
                                PunchOutTime, 
                                StatusId, 
                                Format(Date, 'dd/MM/yyyy') as Date,
                                CASE 
                                    WHEN @RoleId IN (2, 3) THEN (SELECT FirstName + ' ' + LastName FROM Employees WHERE EmployeeId = @id)
                                    ELSE NULL
                                END as Name
                              FROM EmployeeAttendance 
                              WHERE EmployeeId = @id"
                ;

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var attendanceRecords = await connection.QueryAsync<Attendance>(query, new
                    {
                        roleId,
                        id
                    });
                    return attendanceRecords;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Attendance>> UpdateAsync(int id, int userId, Attendance updatedAttendance)
        {
            try
            {
                var query = @"UPDATE EmployeeAttendance 
                              SET PunchInTime = @PunchInTime, PunchOutTime = @PunchOutTime, StatusId = @StatusId, ModifiedBy = @userId, ModifiedDate = GetUtcDate()
                              WHERE EmployeeId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = await connection.ExecuteAsync(query, new
                    {
                        id,
                        updatedAttendance.PunchInTime,
                        updatedAttendance.PunchOutTime,
                        updatedAttendance.StatusId
                    });

                    if (result > 0)
                    {
                        return await UpdateAsync(id, userId, updatedAttendance);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task DeleteAsync(int id, int roleId)
        {
            if (roleId != 3)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this record.");
            }

            try
            {
                var query = @"DELETE FROM EmployeeAttendance WHERE EmployeeId = @id";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.ExecuteAsync(query, new
                    {
                        id
                    });
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
