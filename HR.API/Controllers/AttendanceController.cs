using HR.Domain.DTOs.Attendance;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Attendances")]

    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceServices _attendanceservices;
        public AttendanceController(IAttendanceServices attendanceservices)
        {
            _attendanceservices = attendanceservices;
        }

        #region Record employee clock-in time.

        [HttpPost("clock-in")]
        [SwaggerOperation(Summary = "Record employee clock-in time", OperationId = "ClockIn")]

        public async Task<IActionResult> ClockIn([FromBody] ClockInDTO clockInDTO)
        {
            if (ModelState.IsValid)
            {
                string result = await _attendanceservices.ClockIn(clockInDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Record employee clock-out time.

        [HttpPost("clock-out")]
        [SwaggerOperation(Summary = "Record employee clock-out time", OperationId = "Clockout")]

        public async Task<IActionResult> Clockout([FromBody] ClockOutDTO clockoutDTO)
        {
            if (ModelState.IsValid)
            {
                string result = await _attendanceservices.ClockOut(clockoutDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Retrieve attendance records for a specific employee.

        [HttpGet("{employeeId}")]
        [SwaggerOperation(Summary = "Retrieve attendance records for a specific employee", OperationId = "GetAttendanceByEmployee")]

        public async Task<IActionResult> GetAttendanceByEmployee(string employeeId)
        {
            var attendanceRecords = await _attendanceservices.GetAttendanceById(employeeId);
            return Ok(attendanceRecords);
        }
        #endregion

        #region Retrieve attendance for all employees on a specific date.

        [HttpGet("daily")]
        [SwaggerOperation(Summary = "Retrieve attendance for all employees on a specific date", OperationId = "GetDailyAttendance")]

        public async Task<IActionResult> GetDailyAttendance([FromQuery] DateOnly date)
        {
            var dailyAttendance = await _attendanceservices.GetDailyAttendanceAsync(date);
            return Ok(dailyAttendance);
        }

        #endregion

        #region Update Attendance (Manager Only)
        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update Attendance (Role : Manager)", OperationId = "updateattendance")]

        public async Task<IActionResult> updateattendance(UpdateAttendanceDTO attendance)
        {
            if (ModelState.IsValid)
            {

                var dailyAttendance = await _attendanceservices.updateAttendanceAsync(attendance);
                return Ok(dailyAttendance);
            }
            else
            { return BadRequest(ModelState); }

        }
        #endregion

        #region Delete Attendance (Manager Only)
        [HttpDelete("Delete")]
        [SwaggerOperation(Summary = "Delete Attendance (Role : Manager)", OperationId = "Deleteattendance")]

        public async Task<IActionResult> Deleteattendance(string EmployeeID)
        {
            if (ModelState.IsValid)
            {

                var dailyAttendance = await _attendanceservices.DeleteAttendanceAsync(EmployeeID);
                return Ok(dailyAttendance);
            }
            else
            { return BadRequest(ModelState); }

        }
        #endregion

    }
}
