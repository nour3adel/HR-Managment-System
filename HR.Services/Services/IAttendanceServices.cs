using HR.Domain.DTOs.Attendance;

namespace HR.Services.Services
{
    public interface IAttendanceServices
    {
        public Task<string> ClockIn(ClockInDTO clockInDTO);
        public Task<string> ClockOut(ClockOutDTO clockoutDTO);
        public Task<IEnumerable<AttendanceRecordDTO>> GetAttendanceById(string EmployeeID);
        public Task<IEnumerable<AttendanceRecordDTO>> GetDailyAttendanceAsync(DateOnly date);
        public Task<string> updateAttendanceAsync(UpdateAttendanceDTO record);
        public Task<string> DeleteAttendanceAsync(string EmployeeID);
    }
}
