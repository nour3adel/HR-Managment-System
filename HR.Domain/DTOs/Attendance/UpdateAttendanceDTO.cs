using HR.Domain.Enums;

namespace HR.Domain.DTOs.Attendance
{
    public class UpdateAttendanceDTO
    {
        public string EmployeeID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly ClockInTime { get; set; }
        public TimeOnly? ClockOutTime { get; set; }
        public AttendanceStatus? Status { get; set; }
    }
}

