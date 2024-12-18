using HR.Domain.DTOs.Attendance;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IAttendanceServices
    {
        public Task<Response<string>> ClockIn(ClockInDTO clockInDTO);
        public Task<Response<string>> ClockOut(ClockOutDTO clockoutDTO);
        public Task<Response<IEnumerable<AttendanceRecordDTO>>> GetAttendanceById(string EmployeeID);
        public Task<Response<IEnumerable<AttendanceRecordDTO>>> GetDailyAttendanceAsync(DateOnly date);
        public Task<Response<IEnumerable<AttendanceRecordDTO>>> GetAllAttendance();
        public Task<Response<string>> updateAttendanceAsync(UpdateAttendanceDTO record);
        public Task<Response<string>> DeleteAttendanceAsync(string EmployeeID);

    }
}
