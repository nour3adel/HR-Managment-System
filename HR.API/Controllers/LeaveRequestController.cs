using HR.API.Base;
using HR.Domain.DTOs.LeaveRequest;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LeaveRequests")]

    public class LeaveRequestController : AppControllerBase
    {
        private readonly ILeaveRequestServices _services;
        public LeaveRequestController(ILeaveRequestServices services)
        {
            _services = services;
        }

        #region Retrieve all leave requests by an employee
        [Authorize(Roles = "User,Manager,Admin")]
        [HttpGet("employee/{employee_id}")]
        [SwaggerOperation(Summary = "Retrieve all leave requests by an employee", OperationId = "GetAllLeaveRequestsForUser")]
        public async Task<IActionResult> GetAllLeaveRequestsForUser(string employee_id)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.GetLeaveRequestsForEmployee(employee_id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        #endregion

        #region Retrieve all leave requests 
        [Authorize(Roles = "Manager,Admin")]

        [HttpGet("employee/All")]
        [SwaggerOperation(Summary = "Retrieve all leave requests ", OperationId = "GetAllLeaveRequests")]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            if (ModelState.IsValid)
            {
                var result = await _services.GetAllLeaveRequests();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        #endregion

        #region Retrieve details of a specific leave request.
        [Authorize(Roles = "User,Manager,Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieve details of a specific leave request.", OperationId = "GetLeaveRequestByID")]
        public async Task<IActionResult> GetLeaveRequestByID(int id)
        {
            if (ModelState.IsValid)
            {
                Response<GetLeaveRequestDTO> result = await _services.GetLeaveRequestById(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        #endregion

        #region Create a new leave request.
        [Authorize(Roles = "User")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new leave request", OperationId = "CreateLeaveRequest")]

        public async Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestDTO createLeaveRequestDTO)
        {

            if (ModelState.IsValid)
            {
                Response<string> result = await _services.CreateLeaveRequest(createLeaveRequestDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        #endregion

        #region Approve a leave request (manager only).
        [Authorize(Roles = "Manager,Admin")]

        [HttpPut("{id}/approve")]
        [SwaggerOperation(Summary = "Approve a leave request (Role : Manager).", OperationId = "ApproveLeaveRequest")]
        public async Task<IActionResult> ApproveLeaveRequest(int id)
        {
            if (ModelState.IsValid)
            {
                Response<string> result = await _services.Approve(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        #endregion

        #region Reject a leave request (manager only).

        [Authorize(Roles = "Manager,Admin")]

        [HttpPut("{id:int}/reject")]
        [SwaggerOperation(Summary = "Reject a leave request (Role : Manager).", OperationId = "RejectLeaveRequest")]
        public async Task<IActionResult> RejectLeaveRequest(int id)
        {
            if (ModelState.IsValid)
            {
                Response<string> result = await _services.Reject(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        #endregion

        #region Delete a leave request (manager only).

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:int}/Delete")]
        [SwaggerOperation(Summary = "Delete a leave request (Role : Manager).", OperationId = "DeleteLeaveRequest")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            if (ModelState.IsValid)
            {
                Response<string> result = await _services.DeleteLeaveRequest(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        #endregion
    }
}
