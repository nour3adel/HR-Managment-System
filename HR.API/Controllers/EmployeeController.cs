using HR.API.Base;
using HR.Domain.DTOs.Employee;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Employees")]

    public class EmployeeController : AppControllerBase
    {
        private readonly IEmployeeServices employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;

        }


        #region Get All Employees

        [SwaggerOperation(Summary = "Get All Employees", OperationId = "GetAllEmployees")]
        [HttpGet("Employees")]

        public async Task<IActionResult> GetAllEmployees()
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.GetAllEmployees();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Get All Users

        [SwaggerOperation(Summary = "Get All Users", OperationId = "GetAllUser")]
        [HttpGet("Users")]

        public async Task<IActionResult> GetAllUser()
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.GetAllUsers();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Get All Managers
        [Authorize(Roles = "User,Manager")]
        [SwaggerOperation(Summary = "Get All Managers", OperationId = "GetAllManagers")]
        [HttpGet("Admins")]

        public async Task<IActionResult> GetAllManagers()
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.GetAllManagers();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Get Customer By ID


        [SwaggerOperation(Summary = "Get Customer By ID ", OperationId = "GetUserByID")]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserByID(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.GetCustomerByID(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Register New User

        [SwaggerOperation(Summary = "Register New User", OperationId = "RegisterUser")]
        [HttpPost("Register")]

        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.RegisterUser(registerUserDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Edit User

        [SwaggerOperation(Summary = "Edit User", OperationId = "EditUser")]
        [HttpPut("EditUser")]

        public async Task<IActionResult> EditUser([FromBody] EditCutomerDTO editCutomerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.EditUser(editCutomerDTO);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Delete Employee By ID


        [SwaggerOperation(Summary = "Delete Employee By ID ", OperationId = "DeleteUser")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.DeleteUser(id);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Change Employee Password

        [Authorize]
        [HttpPost("changepassword")]
        [SwaggerOperation(Summary = "Change Employee Password")]


        public async Task<IActionResult> changepassword(ChangePasswordDTO changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeServices.ChangePassword(changePassword);
                return NewResult(result);
            }
            return BadRequest(ModelState);

        }
        #endregion

    }
}
