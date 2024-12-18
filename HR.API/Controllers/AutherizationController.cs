using HR.API.Base;
using HR.Domain.DTOs.Autherization;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Autherization")]

    public class AutherizationController : AppControllerBase
    {
        private readonly IAutherizationServices autherization;

        public AutherizationController(IAutherizationServices autherization)
        {
            this.autherization = autherization;

        }

        #region Get All Roles
        [Authorize(Roles = "Admin,Manager")]
        [SwaggerOperation(Summary = "Get All Roles", OperationId = "Getall")]
        [HttpGet("Role")]

        public async Task<IActionResult> Getall()
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.GetAllRoles();
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Get Roles for specific User 
        [Authorize(Roles = "User,Manager,Admin")]
        [SwaggerOperation(Summary = "Get Roles for specific User ", OperationId = "GetrolesforUser")]
        [HttpGet("Role/{userId}")]

        public async Task<IActionResult> GetrolesforUser(string userId)
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.GetRolesForUser(userId);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Add New Role
        [Authorize(Roles = "Admin,Manager")]
        [SwaggerOperation(Summary = "Add New Role", OperationId = "AddRole")]
        [HttpPost("Role")]

        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.AddNewRole(roleName);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Add Role To Specific User
        //[Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add Role To Specific User", OperationId = "AddRoleToUser")]
        [HttpPost("EmployeeRole")]

        public async Task<IActionResult> AddRoleToUser(string userID, string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.AddRoleToEmployee(userID, roleName);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Edit Role
        [Authorize(Roles = "Admin,Manager")]
        [SwaggerOperation(Summary = "Edit Role", OperationId = "EditRole")]
        [HttpPut("Role")]

        public async Task<IActionResult> EditRole([FromBody] EditRoleDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.EditRoleName(dto);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Remove Role
        [Authorize(Roles = "Admin,Manager")]
        [SwaggerOperation(Summary = "Remove Role", OperationId = "RemoveRole")]
        [HttpDelete("Role/{roleName}")]

        public async Task<IActionResult> RemoveRole(string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await autherization.RemoveRole(roleName);
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
