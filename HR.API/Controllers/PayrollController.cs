using HR.API.Base;
using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Payroll")]

    public class PayrollController : AppControllerBase
    {
        private readonly IPayrollServices _payrollservices;
        public PayrollController(IPayrollServices _payrollservices)
        {
            this._payrollservices = _payrollservices;
        }

        [HttpGet("{Employeeid}")]
        [SwaggerOperation(Summary = "Get payroll details by Employee ID", OperationId = "Getbyemplyeeid")]
        [ProducesResponseType(typeof(Payroll), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Getbyemplyeeid(string Employeeid)
        {
            var result = await _payrollservices.GetPayrollbyEmployeeid(Employeeid);
            return NewResult(result);
        }

        [HttpGet("{month}/{year}")]
        [SwaggerOperation(Summary = "Get payroll details by month and year", OperationId = "Getbydate")]
        [ProducesResponseType(typeof(Payroll), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Getbydate(int month, int year)
        {
            var result = await _payrollservices.GetPayrollbyDate(month, year);
            return NewResult(result);
        }

        [HttpGet("{Employeeid}/{month}/{year}")]
        [SwaggerOperation(Summary = "Get payroll details by Employee ID, month, and year", OperationId = "Getbydateforemployee")]
        [ProducesResponseType(typeof(Payroll), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Getbydateforemployee(string Employeeid, int month, int year)
        {
            var result = await _payrollservices.GetPayrollbyDateforEmployee(Employeeid, month, year);
            return NewResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new payroll for an employee", OperationId = "Add")]
        [ProducesResponseType(typeof(Payroll), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(AddPayrollDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollservices.AddPayrollforEmployee(dto);
                return NewResult(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Calculate")]
        [SwaggerOperation(Summary = "Calculate payroll for an employee based on given dates", OperationId = "Calculate")]
        [ProducesResponseType(typeof(Payroll), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Calculate(PayrollDateDTO dto)
        {
            var result = await _payrollservices.CalculatePayroll(dto);
            return NewResult(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit payroll details for an employee", OperationId = "Edit")]
        [ProducesResponseType(typeof(Payroll), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]

        public async Task<IActionResult> Edit(int id, EditPayrollDTO editPayrollDTO)
        {
            if (id != editPayrollDTO.Id)
            {
                return BadRequest("Not the same id");
            }
            if (ModelState.IsValid)
            {
                var result = await _payrollservices.UpdatePayrollforEmployee(editPayrollDTO);
                return NewResult(result);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{Employeeid}")]
        [SwaggerOperation(Summary = "Delete payroll details for an employee", OperationId = "Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string Employeeid)
        {
            var result = await _payrollservices.DeletePayrollforemployee(Employeeid);
            return NewResult(result);
        }
    }
}