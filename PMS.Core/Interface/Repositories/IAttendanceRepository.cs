using PMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Interface.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAttendance(int designationId, int id);
        Task<IEnumerable<Attendance>> UpdateAsync(int id, int userId, Attendance updatedAttendance);

    }
}
