using HR.Domain.Classes;
using HR.Domain.DTOs.LeaveRequest;
using HR.Domain.Enums;
using HR.Domain.Helpers;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly DbSet<LeaveRequest> _leaveRequests;
        public LeaveRequestRepository(HRdbContext dbContext) : base(dbContext)
        {
            _leaveRequests = dbContext.Set<LeaveRequest>();

        }

        public async Task<IEnumerable<GetLeaveRequestDTO>> GetAllLeaveRequests()
        {
            List<LeaveRequest> leaveRequests = await _leaveRequests.ToListAsync();
            // Now map the status outside the query expression
            var result = leaveRequests.Select(a => new GetLeaveRequestDTO
            {
                Id = a.Id,
                EmployeeName = a.Employee.FullName,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Description = a.Description,
                Status = EnumString.MapEnumToString<LeaveRequestStatus>(a.Status)
            }).ToList();

            return result;
        }

        public async Task<IEnumerable<GetLeaveRequestDTO>> GetLeaveRequests(string EmployeeID)
        {
            List<LeaveRequest> leaveRequests = await _leaveRequests.Where(x => x.EmployeeId.Equals(EmployeeID)).ToListAsync();
            // Now map the status outside the query expression
            var result = leaveRequests.Select(a => new GetLeaveRequestDTO
            {
                Id = a.Id,

                EmployeeName = a.Employee.FullName,

                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Description = a.Description,
                Status = EnumString.MapEnumToString<LeaveRequestStatus>(a.Status)
            }).ToList();

            return result;

        }
    }
}