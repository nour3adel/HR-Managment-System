using System.ComponentModel.DataAnnotations;

namespace HR.Domain.DTOs.PerformanceReview
{
    public class AddPerformanceReviewDTO
    {

        [MaxLength(500)]
        public string? Review { get; set; }
        [Range(1, 10)]
        public int RatingScore { get; set; }
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
    }
}
