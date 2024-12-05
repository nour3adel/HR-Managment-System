using HR.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR.Domain.DTOs.Notifications
{
    public class SendDTO
    {

        public string subject { get; set; }
        public string EmployeeId { get; set; }

        [MaxLength(500)]
        public string? MessageContent { get; set; }

        public DateTime Date { get; set; }

        public NotificationType Type { get; set; }

        public bool isUrgent { get; set; }

    }
}
