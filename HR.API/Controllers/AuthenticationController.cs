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
    [ApiExplorerSettings(GroupName = "Authentication")]

    public class AuthenticationController : AppControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;

        }
        #region Validate access Token
        [Authorize(Roles = "User,Manager,Admin")]
        [SwaggerOperation(Summary = "Validate access Token", OperationId = "ValidateToken")]
        [HttpGet("Validate-Token")]

        public async Task<IActionResult> ValidateToken(string accessToken)
        {
            if (ModelState.IsValid)
            {
                var result = await authenticationService.ValidateToken(accessToken);
                return NewResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Login Employee

        [SwaggerOperation(Summary = "Login Employee", OperationId = "LoginUser")]
        [HttpPost("Login")]

        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginUserDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await authenticationService.Login(loginUserDTO);
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
