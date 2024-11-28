using HR.API.Base;
using HR.Domain.DTOs.Payroll;
using HR.Domain.DTOs.PerformanceReview;
using HR.Services.Implementations;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "PerformanceReview")]

    public class PerformanceReviewController : AppControllerBase
    {
        private readonly IPerformanceReviewServices _performanceReviewServices;
        public PerformanceReviewController(IPerformanceReviewServices performanceReviewServices)
        {
            this._performanceReviewServices = performanceReviewServices;
        }
        [HttpGet("{Employeeid}")]
        public async Task<IActionResult> Getbyemplyeeid(string Employeeid)
        {
            var result = await _performanceReviewServices.GetPerformanceReviewbyEmployeeid(Employeeid);
            return NewResult(result);
        }
        [HttpGet("{Employeeid}/{date}")]
        public async Task<IActionResult> Getbydateforemployee(string Employeeid, DateOnly date)
        {
            var result = await _performanceReviewServices.GetPerformanceReviewbyDateforEmployee(Employeeid, date);
            return NewResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPerformanceReviewDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _performanceReviewServices.AddPerformanceReviewforEmployee(dto);
                return NewResult(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("Employeeid/{id}")]
        public async Task<IActionResult> Editforemployee(int id, EditPerformanceReviewDTO editPerformanceReview)
        {
            if (id != editPerformanceReview.Id)
            {
                return BadRequest("Not the same id");
            }
            if (ModelState.IsValid)
            {
                var result = await _performanceReviewServices.UpdatePerformanceReviewforEmployee(editPerformanceReview);
                return NewResult(result);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EditPerformanceReviewDTO editPerformanceReview)
        {
            if (id != editPerformanceReview.Id)
            {
                return BadRequest("Not the same id");
            }
            if (ModelState.IsValid)
            {
                var result = await _performanceReviewServices.UpdatePerformanceReview(editPerformanceReview);
                return NewResult(result);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpDelete("Emplyoeeid/{Employeeid}")]
        public async Task<IActionResult> Deleteforemployee(string Employeeid)
        {
            var result = await _performanceReviewServices.DeletePerformanceReviewforemployee(Employeeid);
            return NewResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _performanceReviewServices.DeletePerformance(id);
            return NewResult(result);
        }
    }
}
