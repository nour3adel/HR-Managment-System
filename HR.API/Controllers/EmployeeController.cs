using HR.Domain.DTOs.Employee;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;

        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
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

    }
}
