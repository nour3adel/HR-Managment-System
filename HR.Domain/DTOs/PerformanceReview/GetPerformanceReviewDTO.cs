namespace HR.Domain.DTOs.PerformanceReview
{
    public class GetPerformanceReviewDTO
    {
        public int Id { get; set; }
        public string? Review { get; set; }
        public int RatingScore { get; set; }
        public string EmployeeName { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
