using HR.Domain.DTOs.Notifications;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface INotificationServices
    {
        public Task<Response<string>> Send(SendDTO send);
        public Task<Response<IEnumerable<GetNotificationDTO>>> GetByID(string empID);
    }
}
