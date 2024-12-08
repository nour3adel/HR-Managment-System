namespace HR.Domain.DTOs.Notifications
{
    public class GetNotificationDTO
    {
        public int Id { get; set; }
        public string titlee { get; set; }

        public string? MessageContent { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public bool isUrgent { get; set; }

    }
}
