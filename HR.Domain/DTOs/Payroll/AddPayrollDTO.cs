using System.ComponentModel.DataAnnotations;

namespace HR.Domain.DTOs.Payroll
{
    public class AddPayrollDTO
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        [Range(2020, 2024)]
        public int Year { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
    }
}
