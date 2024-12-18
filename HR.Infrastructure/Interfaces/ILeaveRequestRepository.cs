using HR.Domain.Classes;
using HR.Domain.DTOs.LeaveRequest;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        public Task<IEnumerable<GetLeaveRequestDTO>> GetLeaveRequests(string EmployeeID);
        public Task<IEnumerable<GetLeaveRequestDTO>> GetAllLeaveRequests();

    }
}
