using HR.Domain.Classes;
using HR.Domain.DTOs.Notifications;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        public Task<IEnumerable<GetNotificationDTO>> GetNotifications(string EmployeeID);

    }
}
