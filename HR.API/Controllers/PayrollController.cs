using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Payroll")]

    public class PayrollController : ControllerBase
    {
        private readonly IPayrollServices _payrollservices;
        public PayrollController(IPayrollServices _payrollservices)
        {
            this._payrollservices = _payrollservices;
        }
        [HttpGet("{Employeeid}")]
        public async Task<IActionResult> Getbyemplyeeid(string Employeeid)
        {
            var result = await _payrollservices.GetPayrollbyEmployeeid(Employeeid);
            if (!result.Any())
            {
                return BadRequest($"There is No Payroll History for Embloyee with id: {Employeeid}");
            }
            return Ok(result);
        }
        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> Getbydate(int month, int year)
        {
            var result = await _payrollservices.GetPayrollbyDate(month, year);
            var reslist = result.ToList();
            if (reslist.Count == 0)
            {
                return BadRequest($"There is No Payroll History for month: {month} in year: {year}");
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPayrollDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollservices.AddPayrollforEmployee(dto);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Calculate")]
        public async Task<IActionResult> Calculate(PayrollDateDTO dto)
        {
            var result = await _payrollservices.CalculatePayroll(dto);
            return Ok("Totla Payroll is " + result +" LE");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EditPayrollDTO editPayrollDTO)
        {
            if(id != editPayrollDTO.Id)
            {
                return BadRequest("Not the same id");
            }
            if (ModelState.IsValid)
            {
                var result = await _payrollservices.UpdatePayrollforEmployee(editPayrollDTO);
                return Ok(result);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpDelete("{Employeeid}")]
        public async Task<IActionResult> Delete(string Employeeid)
        {
            var result = await _payrollservices.DeletePayrollforemployee(Employeeid);
            return Ok(result);
        }
    }
}
