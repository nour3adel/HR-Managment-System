using HR.Domain.Classes;
using HR.Domain.DTOs.LeaveRequest;
using HR.Domain.Enums;
using HR.Domain.Helpers;
using HR.Infrastructure.Interfaces;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;

namespace HR.Services.Implementations
{
    public class LeaveRequestServices : ResponseHandler, ILeaveRequestServices
    {
        #region Fields

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly UserManager<Employee> _userManager;

        #endregion

        #region Constructor
        public LeaveRequestServices(ILeaveRequestRepository leaveRequestRepository,
                                    UserManager<Employee> userManager)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _userManager = userManager;
        }

        #endregion

        #region Create Leave Request Service
        public async Task<Response<string>> CreateLeaveRequest(CreateLeaveRequestDTO createLeaveRequestDTO)
        {
            // Validate Employee existence
            var employee = await _userManager.FindByIdAsync(createLeaveRequestDTO.EmployeeId);
            if (employee == null)
                return NotFound<string>("Employee does not exist");

            // Validate date range
            if (createLeaveRequestDTO.EndDate < createLeaveRequestDTO.StartDate)
                return BadRequest<string>("End date must be after the start date");

            // Map DTO to Entity
            LeaveRequest leaveRequest = new LeaveRequest()
            {

                EmployeeId = createLeaveRequestDTO.EmployeeId,
                StartDate = createLeaveRequestDTO.StartDate,
                EndDate = createLeaveRequestDTO.EndDate,
                Description = createLeaveRequestDTO.Description,
                Status = LeaveRequestStatus.Pending,
            };

            // Add to repository
            await _leaveRequestRepository.AddAsync(leaveRequest);


            return Created("Created Successfully");
        }
        #endregion

        #region Get Leave Request By ID Service
        public async Task<Response<GetLeaveRequestDTO>> GetLeaveRequestById(int id)
        {
            LeaveRequest lrequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (lrequest == null) return NotFound<GetLeaveRequestDTO>($"Leave Request With Id = {id}  Not Exist");
            GetLeaveRequestDTO leaveRequestDTO = new GetLeaveRequestDTO()
            {
                Id = lrequest.Id,
                EmployeeName = lrequest.Employee.FullName,

                StartDate = lrequest.StartDate,
                EndDate = lrequest.EndDate,
                Description = lrequest.Description,
                Status = EnumString.MapEnumToString<LeaveRequestStatus>(lrequest.Status)
            };
            return Success(leaveRequestDTO);
        }
        #endregion

        #region Approve LeaveRequest Service
        public async Task<Response<string>> Approve(int id)
        {
            LeaveRequest lrequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (lrequest == null) return NotFound<string>($"Leave Request With Id = {id}  Not Exist");
            lrequest.Status = LeaveRequestStatus.Approved;
            await _leaveRequestRepository.UpdateAsync(lrequest);
            return Updated<string>("Approved Successfuly");


        }
        #endregion

        #region Reject LeaveRequest Service
        public async Task<Response<string>> Reject(int id)
        {
            LeaveRequest lrequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (lrequest == null) return NotFound<string>($"Leave Request With Id = {id}  Not Exist");
            lrequest.Status = LeaveRequestStatus.Rejected;
            await _leaveRequestRepository.UpdateAsync(lrequest);
            return Updated<string>("Rejected Successfuly");

        }
        #endregion

        #region Get All LeaveRequests For Specific Employee Service
        public async Task<Response<IEnumerable<GetLeaveRequestDTO>>> GetLeaveRequestsForEmployee(string EmployeeID)
        {
            // Validate Employee existence
            var employee = await _userManager.FindByIdAsync(EmployeeID);
            if (employee == null)
                return NotFound<IEnumerable<GetLeaveRequestDTO>>("Employee does not exist");

            var leaverequests = await _leaveRequestRepository.GetLeaveRequests(EmployeeID);

            return Success(leaverequests);
        }

        #endregion  

        #region Delete Leave Request Service
        public async Task<Response<string>> DeleteLeaveRequest(int id)
        {
            LeaveRequest lrequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (lrequest == null) return NotFound<string>($"Leave Request With Id = {id}  Not Exist");
            lrequest.Status = LeaveRequestStatus.Rejected;
            await _leaveRequestRepository.DeleteAsync(lrequest);
            return Deleted<string>("Deleted Successfuly");
        }


        #endregion
        #region Get ALL Leave Request Service
        public async Task<Response<IEnumerable<GetLeaveRequestDTO>>> GetAllLeaveRequests()
        {
            var lrequest = await _leaveRequestRepository.GetAllLeaveRequests();
            if (lrequest == null) return NotFound<IEnumerable<GetLeaveRequestDTO>>("No Leave Request Found");

            return Success(lrequest);
        }
        #endregion
    }
}
