using HR.Domain.Classes;
using HR.Domain.DTOs.Notifications;
using HR.Infrastructure.Interfaces;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;

namespace HR.Services.Implementations
{
    public class NotificationServices : ResponseHandler, INotificationServices
    {
        private readonly UserManager<Employee> _userManager;
        private readonly INotificationRepository _notificationRepository;
        public NotificationServices(UserManager<Employee> userManager, INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }



        public async Task<Response<string>> Send(SendDTO send)
        {
            // validate user is exist
            var existUser = await _userManager.FindByIdAsync(send.EmployeeId);
            if (existUser == null)
                return NotFound<string>($"User With ID = {send.EmployeeId} does not exist");
            Notification notification = new Notification()
            {
                EmployeeId = send.EmployeeId,
                subject = send.subject,
                MessageContent = send.MessageContent,
                Type = send.Type,
                isUrgent = send.isUrgent,
                Date = send.Date
            };
            await _notificationRepository.AddAsync(notification);

            return Created("Notification sent successfully.");
        }

        public async Task<Response<IEnumerable<GetNotificationDTO>>> GetByID(string empID)
        {
            // validate user is exist
            var existUser = await _userManager.FindByIdAsync(empID);
            if (existUser == null)
                return NotFound<IEnumerable<GetNotificationDTO>>($"User With ID = {empID} does not exist");
            var notification = await _notificationRepository.GetNotifications(empID);
            if (notification == null)
            {
                return NotFound<IEnumerable<GetNotificationDTO>>("No Notifications Found");
            }
            return Success(notification);


        }

        public async Task<Response<IEnumerable<GetNotificationDTO>>> GetAll()
        {

            var notification = await _notificationRepository.GetAllNotifications();
            if (notification == null)
            {
                return NotFound<IEnumerable<GetNotificationDTO>>("No Notifications Found");
            }
            return Success(notification);
        }
    }
}
