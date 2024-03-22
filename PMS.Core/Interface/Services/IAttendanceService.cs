using PMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Interface.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAttendance(int roleId, int id);
        Task<IEnumerable<Attendance>> UpdateAsync(int id, int userId, Attendance updatedattendance);

    }
}
