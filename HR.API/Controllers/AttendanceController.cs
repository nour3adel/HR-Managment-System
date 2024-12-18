using HR.API.Base;
using HR.Domain.DTOs.Attendance;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Attendances")]

    public class AttendanceController : AppControllerBase
    {
        private readonly IAttendanceServices _attendanceservices;
        public AttendanceController(IAttendanceServices attendanceservices)
        {
            _attendanceservices = attendanceservices;
        }

        #region Retrieve attendance records for all employee. (Manager Only)
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve attendance records for all employee", OperationId = "GetAllAttendance")]

        public async Task<IActionResult> GetAllAttendance()
        {
            var attendanceRecords = await _attendanceservices.GetAllAttendance();
            return NewResult(attendanceRecords);
        }
        #endregion

        #region Retrieve attendance records for a specific employee.

        [Authorize(Roles = "Admin,User,Manager")]
        [HttpGet("{employeeId}")]
        [SwaggerOperation(Summary = "Retrieve attendance records for a specific employee", OperationId = "GetAttendanceByEmployee")]

        public async Task<IActionResult> GetAttendanceByEmployee(string employeeId)
        {
            var attendanceRecords = await _attendanceservices.GetAttendanceById(employeeId);
            return NewResult(attendanceRecords);
        }
        #endregion

        #region Retrieve attendance for all employees on a specific date. (Manager Only)
        [Authorize(Roles = "Admin")]
        [HttpGet("daily")]
        [SwaggerOperation(Summary = "Retrieve attendance for all employees on a specific date", OperationId = "GetDailyAttendance")]
        public async Task<IActionResult> GetDailyAttendance([FromQuery] string date)
        {
            if (DateOnly.TryParse(date, out DateOnly dateValue))
            {
                var dailyAttendance = await _attendanceservices.GetDailyAttendanceAsync(dateValue);
                return NewResult(dailyAttendance);
            }
            else
            {
                return BadRequest("Invalid date format. Please use yyyy-MM-dd.");
            }
        }

        #endregion

        #region Record employee clock-in time.
        [Authorize(Roles = "User")]
        [HttpPost("clock-in")]
        [SwaggerOperation(Summary = "Record employee clock-in time", OperationId = "ClockIn")]

        public async Task<IActionResult> ClockIn([FromBody] ClockInDTO clockInDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _attendanceservices.ClockIn(clockInDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Record employee clock-out time.
        [Authorize(Roles = "User")]
        [HttpPost("clock-out")]
        [SwaggerOperation(Summary = "Record employee clock-out time", OperationId = "Clockout")]

        public async Task<IActionResult> Clockout([FromBody] ClockOutDTO clockoutDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _attendanceservices.ClockOut(clockoutDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Update Attendance (Admin Only)

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update Attendance (Role : Manager)", OperationId = "updateattendance")]

        public async Task<IActionResult> updateattendance(UpdateAttendanceDTO attendance)
        {
            if (ModelState.IsValid)
            {

                var dailyAttendance = await _attendanceservices.updateAttendanceAsync(attendance);
                return NewResult(dailyAttendance);
            }
            else
            { return BadRequest(ModelState); }

        }
        #endregion

        #region Delete Attendance (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        [SwaggerOperation(Summary = "Delete Attendance (Role : Manager)", OperationId = "Deleteattendance")]

        public async Task<IActionResult> Deleteattendance(string EmployeeID)
        {
            if (ModelState.IsValid)
            {

                var dailyAttendance = await _attendanceservices.DeleteAttendanceAsync(EmployeeID);
                return NewResult(dailyAttendance);
            }
            else
            { return BadRequest(ModelState); }

        }
        #endregion

    }
}
