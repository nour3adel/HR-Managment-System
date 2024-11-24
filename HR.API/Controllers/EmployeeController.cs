using HR.Domain.DTOs.Employee;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Employees")]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;

        }
        #region Register New User

        [SwaggerOperation(Summary = "Register New User", OperationId = "RegisterUser")]
        [HttpPost("Register")]

        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid)
            {
                string result = await employeeServices.RegisterUser(registerUserDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

    }
}
