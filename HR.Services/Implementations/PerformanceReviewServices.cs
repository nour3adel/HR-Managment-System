using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Domain.DTOs.PerformanceReview;
using HR.Infrastructure.Interfaces;
using HR.Infrastructure.Repositories;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.Services.Implementations
{
    public class PerformanceReviewServices : ResponseHandler, IPerformanceReviewServices
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IPerformanceReviewRepository performanceReviewRepository;
        private readonly IMapper mapper;
        public PerformanceReviewServices(UserManager<Employee> _userManager, IPerformanceReviewRepository performanceReviewRepository, IMapper mapper)
        {
            this._userManager = _userManager;
            this.performanceReviewRepository = performanceReviewRepository;
            this.mapper = mapper;
        }
        public async Task<Response<IEnumerable<GetPerformanceReviewDTO>>> GetPerformanceReviewbyEmployeeid(string Employeeid)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<IEnumerable<GetPerformanceReviewDTO>>("Employee does not exist.");
            }
            var performances = await performanceReviewRepository.GetByEmployeeID(Employeeid);
            if (!performances.Any())
            {
                return NotFound<IEnumerable<GetPerformanceReviewDTO>>($"There is No Performancereview History for Embloyee with id: {Employeeid}");
            }
            List<GetPerformanceReviewDTO> PerformanceReviewDTOs = new List<GetPerformanceReviewDTO>();
            foreach (var performance in performances)
            {
                var PerformanceReviewDTO = new GetPerformanceReviewDTO()
                {
                   Id = performance.Id,
                   Review = performance.Review,
                   RatingScore = performance.RatingScore,
                   EmployeeName = performance.Employee.FullName,
                   date = performance.Date
                };
                PerformanceReviewDTOs.Add(PerformanceReviewDTO);
            }
            return Success<IEnumerable<GetPerformanceReviewDTO>>(PerformanceReviewDTOs);
        }
        public async Task<Response<GetPerformanceReviewDTO>> GetPerformanceReviewbyDateforEmployee(string Employeeid, DateOnly date)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<GetPerformanceReviewDTO>("Employee does not exist.");
            }
            var performances = await performanceReviewRepository.GetByDateforEmployee(Employeeid, date);

                var PerformanceReviewDTO = new GetPerformanceReviewDTO()
                {
                    Id = performances.Id,
                    Review = performances.Review,
                    RatingScore = performances.RatingScore,
                    EmployeeName = performances.Employee.FullName,
                    date = performances.Date
                };
            
            if (performances == null)
            {
                return BadRequest<GetPerformanceReviewDTO>($"There is No Payroll History for Employee with id: {Employeeid} for date: {date}");
            }
            return Success<GetPerformanceReviewDTO>(PerformanceReviewDTO);
        }
        public async Task<Response<string>> AddPerformanceReviewforEmployee(AddPerformanceReviewDTO perfreview)
        {
            var user = await _userManager.FindByIdAsync(perfreview.EmployeeId);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var performance = await performanceReviewRepository.GetByIdAsync(perfreview.Id);
            if (performance != null)
            {
                return BadRequest<string>("Performancereview id exist, please Enter valid id");
            }
            var Performance = await performanceReviewRepository.GetByDateforEmployee(perfreview.EmployeeId, perfreview.Date);
            if(Performance != null)
                return BadRequest<string>($"Performancereview exist for employee with id: {perfreview.EmployeeId} with date: {perfreview.Date}");  
            var PerFormance = mapper.Map<PerformanceReview>(perfreview);
            await performanceReviewRepository.AddAsync(PerFormance);
            await performanceReviewRepository.SaveChangesAsync();
            return Created<string>("Created Succesful");
        }
        public async Task<Response<string>> UpdatePerformanceReviewforEmployee(EditPerformanceReviewDTO editPerformanceReview)
        {
            var user = await _userManager.FindByIdAsync(editPerformanceReview.EmployeeId);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }

            var performances = await performanceReviewRepository.GetByEmployeeID(editPerformanceReview.EmployeeId);
            if (performances.Any(p => p.Date == editPerformanceReview.Date && p.Id != editPerformanceReview.Id))
            {
                return BadRequest<string>("There is already a performance review with this date.");
            }

            // Fetch the existing PerformanceReview by Id
            var existingPerformance = await performanceReviewRepository.GetByIdAsync(editPerformanceReview.Id);
            if (existingPerformance == null)
            {
                return NotFound<string>("Performance review not found.");
            }

            // Update the properties
            existingPerformance.RatingScore = editPerformanceReview.RatingScore;
            existingPerformance.Review = editPerformanceReview.Review;
            existingPerformance.Date = editPerformanceReview.Date;

            // Save changes
            await performanceReviewRepository.UpdateAsync(existingPerformance);

            return Updated<string>("Performance review updated successfully.");
        }

        public async Task<Response<string>> UpdatePerformanceReview(EditPerformanceReviewDTO editPerformanceReview)
        {

            var user = await _userManager.FindByIdAsync(editPerformanceReview.EmployeeId);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var performances = await performanceReviewRepository.GetByIdAsync(editPerformanceReview.Id);
            if(performances == null)
                return BadRequest<string>($"there is No performance with this id: {editPerformanceReview.Id}");
            var Performance = mapper.Map<PerformanceReview>(editPerformanceReview);
            await performanceReviewRepository.UpdateAsync(Performance);
            return Updated<string>("Payroll Edited");

        }
        public async Task<Response<string>> DeletePerformanceReviewforemployee(string Employeeid)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var performances = await performanceReviewRepository.GetByEmployeeID(Employeeid);
            if (!performances.Any())
                return NotFound<string>($"There is no Performance history for Emplyee with id: {Employeeid}");
            var resperformances = performances.ToList();
            await performanceReviewRepository.DeleteRangeAsync(resperformances);
            return Deleted<string>($"Performance history for Employee with id: {Employeeid} Deleted successfully");
        }
        public async Task<Response<string>> DeletePerformance(int id)
        {
            var performance = await performanceReviewRepository.GetByIdAsync(id);
            if(performance == null)
                return NotFound<string>($"There is no Performance with id: {id}");
            await performanceReviewRepository.DeleteAsync(performance);
            return Deleted<string>($"Performance with id: {id} Deleted successfully");
        }
    }
}
