using HR.Domain.Classes;
using HR.Domain.DTOs.Notifications;
using HR.Domain.Helpers;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly DbSet<Notification> _notifications;
        public NotificationRepository(HRdbContext dbContext) : base(dbContext)
        {
            _notifications = dbContext.Set<Notification>();
        }

        public async Task<IEnumerable<GetNotificationDTO>> GetNotifications(string EmployeeID)
        {
            List<Notification> notifications = await _notifications.Where(x => x.EmployeeId.Equals(EmployeeID)).ToListAsync();

            var result = notifications.Select(a => new GetNotificationDTO
            {
                Id = a.Id,
                titlee = a.subject,
                MessageContent = a.MessageContent,
                Date = a.Date,
                Type = EnumString.MapNotificationType(a.Type),
                isUrgent = a.isUrgent
            }).ToList();

            return result;
        }

    }
}
