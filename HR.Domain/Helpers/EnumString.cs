using HR.Domain.Enums;

namespace HR.Domain.Helpers
{
    public static class EnumString
    {
        public static string MapAttendanceStatus(AttendanceStatus? status)
        {
            switch (status)
            {
                case AttendanceStatus.Present:
                    return "Present";
                case AttendanceStatus.Absent:
                    return "Absent";
                case AttendanceStatus.Late:
                    return "Late";
                case AttendanceStatus.OnLeave:
                    return "OnLeave";
                default:
                    return "Unknown";
            }
        }

        public static string MapLeaveRequestStatus(LeaveRequestStatus? status)
        {
            switch (status)
            {
                case LeaveRequestStatus.Approved:
                    return "Approved";
                case LeaveRequestStatus.Rejected:
                    return "Rejected";
                case LeaveRequestStatus.Pending:
                    return "Pending";
                default:
                    return "Unknown";
            }
        }
    }
}
