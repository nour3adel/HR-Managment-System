namespace HR.Domain.DTOs.LeaveRequest
{
    public class GetLeaveRequestDTO
    {
        public string EmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }

        public string? Status { get; set; }
    }
}
