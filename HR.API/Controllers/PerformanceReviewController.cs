using HR.API.Base;
using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Domain.DTOs.PerformanceReview;
using HR.Services.Implementations;
using HR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Get performance review details by Employee ID")]
        [ProducesResponseType(typeof(PerformanceReview), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Getbyemplyeeid(string Employeeid)
        {
            var result = await _performanceReviewServices.GetPerformanceReviewbyEmployeeid(Employeeid);
            return NewResult(result);
        }

        [HttpGet("{Employeeid}/{month}/{year}")]
        [SwaggerOperation(Summary = "Get performance review details by Employee ID and date")]
        [ProducesResponseType(typeof(PerformanceReview), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Getbydateforemployee(string Employeeid, int month, int year)
        {
            var result = await _performanceReviewServices.GetPerformanceReviewbyDateforEmployee(Employeeid, month, year);
            return NewResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new performance review for an employee")]
        [ProducesResponseType(typeof(PerformanceReview), 201)]
        [ProducesResponseType(400)]
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
        [SwaggerOperation(Summary = "Edit performance review for an employee")]
        [ProducesResponseType(typeof(PerformanceReview), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        [SwaggerOperation(Summary = "Edit performance review")]
        [ProducesResponseType(typeof(PerformanceReview), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        [SwaggerOperation(Summary = "Delete performance review for an employee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Deleteforemployee(string Employeeid)
        {
            var result = await _performanceReviewServices.DeletePerformanceReviewforemployee(Employeeid);
            return NewResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete performance review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _performanceReviewServices.DeletePerformance(id);
            return NewResult(result);
        }
    }
}
