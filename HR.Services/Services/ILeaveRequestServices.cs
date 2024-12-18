using HR.Domain.DTOs.LeaveRequest;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface ILeaveRequestServices
    {
        public Task<Response<string>> CreateLeaveRequest(CreateLeaveRequestDTO createLeaveRequestDTO);
        public Task<Response<GetLeaveRequestDTO>> GetLeaveRequestById(int id);
        public Task<Response<IEnumerable<GetLeaveRequestDTO>>> GetAllLeaveRequests();
        public Task<Response<string>> Approve(int id);
        public Task<Response<string>> Reject(int id);
        public Task<Response<IEnumerable<GetLeaveRequestDTO>>> GetLeaveRequestsForEmployee(string EmployeeID);

        public Task<Response<string>> DeleteLeaveRequest(int id);


    }
}
