using HR.Domain.Classes;
using HR.Domain.DTOs.Attendance;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        public Task<Attendance> IsExist(string EmployeeID, DateOnly date);
        public Task<IEnumerable<AttendanceRecordDTO>> GetDailyAttendance(DateOnly date);
        public Task<IEnumerable<AttendanceRecordDTO>> GetAttendanceByID(string employeeId);
        public Task<Attendance> GetByEmployeeID(string EmployeeID);


    }
}
