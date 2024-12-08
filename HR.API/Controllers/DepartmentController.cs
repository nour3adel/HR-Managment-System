using HR.API.Base;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Departments")]

    public class DepartmentController : AppControllerBase
    {
        private readonly IDepartmentServices departmentServices;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            this.departmentServices = departmentServices;
        }

        #region Get All Departments
        [HttpGet("GetALL")]
        [SwaggerOperation(summary: "Get All Departments", OperationId = "GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await departmentServices.GetAllDepartments();
            return NewResult(result);
        }
        #endregion

        #region Get Department By ID
        [SwaggerOperation(summary: "Get Department By ID", OperationId = "GetDepartmentByID")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentByID(int id)
        {
            var result = await departmentServices.GetDepartmentByID(id);
            return NewResult(result);
        }
        #endregion
    }
}
