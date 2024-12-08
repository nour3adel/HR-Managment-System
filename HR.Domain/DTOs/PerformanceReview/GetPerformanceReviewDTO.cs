using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.PerformanceReview
{
    public class GetPerformanceReviewDTO
    {
        public int Id { get; set; }
        public string? Review { get; set; }
        public int RatingScore { get; set; }
        public string EmployeeName { get; set; }
        public int month {  get; set; }
        public int year { get; set; }
    }
}
