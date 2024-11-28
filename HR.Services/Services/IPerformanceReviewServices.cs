using HR.Domain.DTOs.Payroll;
using HR.Domain.DTOs.PerformanceReview;
using HR.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services.Services
{
    public interface IPerformanceReviewServices
    {
        public Task<Response<IEnumerable<GetPerformanceReviewDTO>>> GetPerformanceReviewbyEmployeeid(string Employeeid);
        public Task<Response<IEnumerable<GetPerformanceReviewDTO>>> GetPerformanceReviewbyDateforEmployee(string Employeeid, DateOnly date);
        public Task<Response<string>> DeletePerformanceReviewforemployee(string Employeeid);
        public Task<Response<string>> DeletePerformance(int id);
        public Task<Response<string>> AddPerformanceReviewforEmployee(AddPerformanceReviewDTO addPerformanceReview);
        public Task<Response<string>> UpdatePerformanceReviewforEmployee(EditPerformanceReviewDTO editPerformanceReview);
        public Task<Response<string>> UpdatePerformanceReview(EditPerformanceReviewDTO editPerformanceReview);
    }
}
