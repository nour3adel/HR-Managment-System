using HR.Domain.Classes;
using HR.Domain.DTOs.Attendance;
using HR.Domain.Helpers;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class AttendanceRepostory : GenericRepository<Attendance>, IAttendanceRepository
    {
        private DbSet<Attendance> attendances;

        public AttendanceRepostory(HRdbContext dbContext) : base(dbContext)
        {
            attendances = dbContext.Set<Attendance>();
        }
        public async Task<Attendance> IsExist(string EmployeeId, DateOnly date)
        {
            var existingAttendance = await attendances.FirstOrDefaultAsync(a => a.EmployeeId == EmployeeId && a.Date == date);
            return existingAttendance;
        }


        public async Task<IEnumerable<AttendanceRecordDTO>> GetAttendanceByID(string employeeId)
        {
            // Fetch attendance records from the database
            var attendanceRecords = await attendances
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();

            // Now map the status outside the query expression
            var result = attendanceRecords.Select(a => new AttendanceRecordDTO
            {
                EmployeeName = a.Employee.FullName,
                Date = a.Date,
                ClockInTime = a.ClockInTime,
                ClockOutTime = a.ClockOutTime,
                Status = EnumString.MapAttendanceStatus(a.Status)
            }).ToList();

            return result;
        }

        public async Task<IEnumerable<AttendanceRecordDTO>> GetDailyAttendance(DateOnly date)
        {
            // Fetch attendance records for the given date from the database
            var attendanceRecords = await attendances
                .Where(a => a.Date == date)
                .ToListAsync();

            // Now map the status outside the query expression
            var result = attendanceRecords.Select(a => new AttendanceRecordDTO
            {
                EmployeeName = a.Employee.FullName,
                Date = a.Date,
                ClockInTime = a.ClockInTime,
                ClockOutTime = a.ClockOutTime,
                Status = EnumString.MapAttendanceStatus(a.Status)
            }).ToList();

            return result;
        }
        public async Task<Attendance> GetByEmployeeID(string EmployeeID)
        {
            var existingAttendance = await attendances.FirstOrDefaultAsync(a => a.EmployeeId == EmployeeID);
            return existingAttendance;
        }





    }
}
