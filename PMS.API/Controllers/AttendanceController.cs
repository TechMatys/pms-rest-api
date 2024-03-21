using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Core.Interface.Services;
using PMS.Core.Model;
using System.Net;

namespace PMS.API.Controllers
{
    [Route("attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService AttendanceService)
        {
            _attendanceService = AttendanceService ?? throw new ArgumentNullException(nameof(AttendanceService));
        }


        #region Get Attendance

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> Attendance(int roleId, int id)
        {
            var response = await _attendanceService.GetAttendance(roleId, id);

            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "No records found",
                    statusCode = HttpStatusCode.NotFound
                });
            }
            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }
        #endregion

        #region Update Attendance
        [HttpPut("update-attendance")]
        public async Task<ActionResult<Attendance>> UpdateAttendance(int id, int userId, Attendance updatedAttendance)
        {
            var response = await _attendanceService.UpdateAsync(id, userId, updatedAttendance);

            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "No records found",
                    statusCode = HttpStatusCode.NotFound
                });
            }
            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }

        #endregion
    }
}
