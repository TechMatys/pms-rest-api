using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Model
{
    public class Attendance : Users
    {
        public int EmployeeId { get; set; }
        public string? Date { get; set; }
        public string? PunchInTime { get; set; }
        public string? PunchOutTime { get; set; }
        public string? StatusId { get; set; }

    }

}
