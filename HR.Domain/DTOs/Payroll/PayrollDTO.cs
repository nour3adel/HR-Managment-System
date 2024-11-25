using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.Payroll
{
    public class PayrollDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal NetSalary { get; set; }
    }
}
