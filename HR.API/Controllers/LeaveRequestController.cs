using HR.API.Base;
using HR.Domain.DTOs.LeaveRequest;
using HR.Services.Bases;
using HR.Services.Services;
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

        #region Create a new leave request.
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new leave request", OperationId = "CreateLeaveRequest")]

        public async Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestDTO createLeaveRequestDTO)
        {

            if (ModelState.IsValid)
            {
                Response<GetLeaveRequestDTO> result = await _services.CreateLeaveRequest(createLeaveRequestDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        #endregion

        #region Retrieve details of a specific leave request.

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

        #region Approve a leave request (manager only).
        // TODO :: Add Authorize Approve a leave request 

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

        // TODO :: Add Authorize Reject a leave request

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

        #region Retrieve all leave requests by an employee

        [HttpGet("employee/{employee_id}")]
        [SwaggerOperation(Summary = "Retrieve all leave requests by an employee", OperationId = "GetAllLeaveRequests")]
        public async Task<IActionResult> GetAllLeaveRequests(string employee_id)
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

        #region Delete a leave request (manager only).

        // TODO :: Add Authorize Delete a leave request

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
