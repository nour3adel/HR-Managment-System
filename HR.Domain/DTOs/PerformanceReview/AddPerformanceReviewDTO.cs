using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.PerformanceReview
{
    public class AddPerformanceReviewDTO
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Review { get; set; }
        [Range(1, 10)]
        public int RatingScore { get; set; }
        public string EmployeeId{ get; set; }
        public DateOnly date { get; set; }
    }
}
