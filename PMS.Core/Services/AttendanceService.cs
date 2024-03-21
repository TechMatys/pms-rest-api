using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Services
{
    public class AttendanceService : IAttendanceService
    {
        public readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository ?? throw new ArgumentNullException(nameof(attendanceRepository));
        }
        public async Task<IEnumerable<Attendance>> GetAttendance(int roleId, int id)
        {
            return await _attendanceRepository.GetAttendance(roleId, id);
        }

        public async Task<IEnumerable<Attendance>> UpdateAsync(int id, int userId, Attendance updatedAttendance)
        {
            return await _attendanceRepository.UpdateAsync(id, userId, updatedAttendance);
        }
    }
}
