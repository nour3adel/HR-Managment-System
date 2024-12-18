using HR.API.Base;
using HR.Domain.DTOs.Notifications;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Notifications")]

    public class NotificationController : AppControllerBase
    {
        private readonly INotificationServices _notificationservice;

        public NotificationController(INotificationServices notificationservice)
        {
            _notificationservice = notificationservice;

        }

        #region Get All Notification For Specific Employeee
        [Authorize(Roles = "User,Manager,Admin")]
        [SwaggerOperation(Summary = "Get All Notification For Specific Employeee", OperationId = "GetNotificationByID")]
        [HttpGet("{employee_id}")]

        public async Task<IActionResult> GetNotificationByID(string employee_id)
        {
            if (ModelState.IsValid)
            {
                var result = await _notificationservice.GetByID(employee_id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Get All Notification 
        [Authorize(Roles = "Manager,Admin")]
        [SwaggerOperation(Summary = "Get All Notification", OperationId = "GetAllNotification")]
        [HttpGet()]

        public async Task<IActionResult> GetAllNotification()
        {
            if (ModelState.IsValid)
            {
                var result = await _notificationservice.GetAll();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Send Notification
        [Authorize(Roles = "Manager,Admin")]

        [SwaggerOperation(Summary = "Send Notification to Employees ", OperationId = "SendNotification")]
        [HttpPost("Send")]

        public async Task<IActionResult> SendNotification([FromBody] SendDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _notificationservice.Send(dto);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

    }
}
