using HR.Domain.Classes;
using HR.Domain.DTOs.Attendance;
using HR.Domain.Enums;
using HR.Infrastructure.Interfaces;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.Services.Implementations
{
    public class AttendanceService : ResponseHandler, IAttendanceServices
    {
        #region Fields

        private readonly UserManager<Employee> _userManager;
        private readonly IAttendanceRepository _attendanceRepository;

        #endregion

        #region Constructor
        public AttendanceService(UserManager<Employee> userManager, IAttendanceRepository attendanceRepository)
        {
            _userManager = userManager;
            _attendanceRepository = attendanceRepository;
        }
        #endregion

        #region Clock In Service
        public async Task<Response<string>> ClockIn(ClockInDTO clockInDto)
        {
            // Check if the employee exists
            var existUser = await _userManager.FindByIdAsync(clockInDto.EmployeeId);
            if (existUser == null) return NotFound<string>("Employee does not exist.");

            // Get today's date
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            // Retrieve the attendance record for today for the specific employee
            Attendance existingAttendance = await _attendanceRepository.IsExist(clockInDto.EmployeeId, today);
            if (existingAttendance != null)
            {
                return BadRequest<string>("Employee has already clocked in today.");
            }

            // Set the actual clock-in time (current time)
            TimeOnly actualClockInTime = TimeOnly.FromDateTime(DateTime.Now);

            // Create a new attendance record
            Attendance attendance = new Attendance
            {
                EmployeeId = clockInDto.EmployeeId,
                Date = today,
                ClockInTime = actualClockInTime,
                ClockOutTime = null,
                Status = actualClockInTime <= new TimeOnly(8, 0) ? AttendanceStatus.Present : AttendanceStatus.Late // Check if the employee is on time
            };

            // Add the new attendance record to the database
            await _attendanceRepository.AddAsync(attendance);

            return Success("Clock-in recorded successfully.");
        }

        #endregion

        #region Clock Out Service
        public async Task<Response<string>> ClockOut(ClockOutDTO clockoutDTO)
        {
            // Check if employee exists
            var existuser = await _userManager.FindByIdAsync(clockoutDTO.EmployeeId);
            if (existuser == null)
            {
                return NotFound<string>("Employee does not exist.");
            }

            // Get today's date
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            // Retrieve the attendance record for today for the specific employee
            Attendance attendance = await _attendanceRepository.IsExist(clockoutDTO.EmployeeId, today);
            if (attendance == null)
            {
                return BadRequest<string>("Clock-in record not found. Please clock in first.");
            }

            // Check if clock-out time is already set
            if (attendance.ClockOutTime != null)
            {
                return BadRequest<string>("Employee has already clocked out.");
            }

            attendance.ClockOutTime = TimeOnly.FromDateTime(DateTime.Now);

            // Convert TimeOnly to DateTime by assuming today's date (to handle clock-in and clock-out time on the same day)
            DateTime clockInDateTime = DateTime.Today.AddHours(attendance.ClockInTime.Hour).AddMinutes(attendance.ClockInTime.Minute);
            DateTime clockOutDateTime = DateTime.Today.AddHours(attendance.ClockOutTime.Value.Hour).AddMinutes(attendance.ClockOutTime.Value.Minute);

            // Calculate the difference in hours
            TimeSpan timeDifference = clockOutDateTime - clockInDateTime;

            if (timeDifference.TotalHours < 8)
            {
                attendance.Status = AttendanceStatus.LeaveEarly;  // If less than 8 hours, the employee left early
            }
            else
            {
                attendance.Status = AttendanceStatus.Present;  // If 8 hours or more, the employee is present
            }



            // Save changes
            await _attendanceRepository.SaveChangesAsync();

            return Success("Clock-out recorded successfully.");
        }

        #endregion

        #region Get Attendance By ID Service
        public async Task<Response<IEnumerable<AttendanceRecordDTO>>> GetAttendanceById(string employeeId)
        {
            // Check if the employee exists
            var existUser = await _userManager.FindByIdAsync(employeeId);
            if (existUser == null) return NotFound<IEnumerable<AttendanceRecordDTO>>("Employee does not exist.");

            var result = await _attendanceRepository.GetAttendanceByID(employeeId);

            return Success(result);
        }

        #endregion

        #region Get Daily Attendance Service
        public async Task<Response<IEnumerable<AttendanceRecordDTO>>> GetDailyAttendanceAsync(DateOnly date)
        {
            // Retrieve all employees
            var employees = await _userManager.Users.ToListAsync();

            // Retrieve attendance records for the given date
            var attendanceRecords = await _attendanceRepository.GetDailyAttendance(date);

            // Check if there are no attendance records for the given date
            if (!attendanceRecords.Any())
            {
                return NotFound<IEnumerable<AttendanceRecordDTO>>("No attendance on that day.");
            }

            // Combine the data, ensuring all employees are included
            var result = employees.Select(employee =>
            {
                var attendance = attendanceRecords.FirstOrDefault(a => a.EmployeeName == employee.FullName);

                // Create the AttendanceRecordDTO for each employee
                return new AttendanceRecordDTO
                {
                    EmployeeName = employee.FullName,
                    Date = date,
                    ClockInTime = attendance?.ClockInTime ?? default,
                    ClockOutTime = attendance?.ClockOutTime ?? default,
                    Status = attendance != null
                        ? attendance.Status
                        : "Absent"
                };
            }).ToList();

            return Success<IEnumerable<AttendanceRecordDTO>>(result);
        }

        #endregion

        #region Get All Attendance Service
        public async Task<Response<IEnumerable<AttendanceRecordDTO>>> GetAllAttendance()
        {

            var attendanceRecords = await _attendanceRepository.GetAllAttendance();



            return Success<IEnumerable<AttendanceRecordDTO>>(attendanceRecords);
        }
        #endregion

        #region Update Attendance Service
        public async Task<Response<string>> updateAttendanceAsync(UpdateAttendanceDTO record)
        {

            var user = await _userManager.FindByIdAsync(record.EmployeeID);
            if (user == null)
            {
                return NotFound<string>("User does not exist.");
            }

            var existingAttendance = await _attendanceRepository.IsExist(user.Id, record.Date);
            if (existingAttendance == null)
            {
                return NotFound<string>("No attendance record found for the given date.");
            }


            existingAttendance.ClockInTime = record.ClockInTime;
            if (record.ClockOutTime.HasValue && record.ClockOutTime < record.ClockInTime)
            {
                return BadRequest<string>("Clock-out time cannot be earlier than clock-in time.");
            }
            existingAttendance.ClockOutTime = record.ClockOutTime;

            existingAttendance.Status = record.Status;


            await _attendanceRepository.UpdateAsync(existingAttendance);


            return Updated<string>("Attendance Records Updated");
        }

        #endregion

        #region Delete Attendance Service
        public async Task<Response<string>> DeleteAttendanceAsync(string EmployeeID)
        {
            var user = await _userManager.FindByIdAsync(EmployeeID);
            if (user == null)
            {
                return NotFound<string>("User does not exist.");
            }

            Attendance existingAttendance = await _attendanceRepository.GetByEmployeeID(EmployeeID);
            if (existingAttendance == null)
            {
                return NotFound<string>("No attendance record found for the given date.");
            }

            await _attendanceRepository.DeleteAsync(existingAttendance);
            return Deleted<string>("Deleted Successfully");
        }




        #endregion




    }
}
