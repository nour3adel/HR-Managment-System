using HR.Domain.DTOs.PerformanceReview;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IPerformanceReviewServices
    {
        public Task<Response<IEnumerable<GetPerformanceReviewDTO>>> GetPerformanceReviewbyEmployeeid(string Employeeid);
        public Task<Response<GetPerformanceReviewDTO>> GetPerformanceReviewbyDateforEmployee(string Employeeid, int month, int year);
        public Task<Response<string>> DeletePerformanceReviewforemployee(string Employeeid);
        public Task<Response<string>> DeletePerformance(int id);
        public Task<Response<string>> AddPerformanceReviewforEmployee(AddPerformanceReviewDTO addPerformanceReview);
        public Task<Response<string>> UpdatePerformanceReviewforEmployee(EditPerformanceReviewDTO editPerformanceReview);
        public Task<Response<string>> UpdatePerformanceReview(EditPerformanceReviewDTO editPerformanceReview);
    }
}
