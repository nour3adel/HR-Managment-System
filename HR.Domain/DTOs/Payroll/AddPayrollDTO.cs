using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.Payroll
{
    public class AddPayrollDTO
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
    }
}
