using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.DTOs.Payroll;
namespace HR.Services.Services
{
    public interface IPayrollServices
    {
        public Task<IEnumerable<PayrollDTO>> GetPayrollbyEmployeeid(string Employeeid);
        public Task<IEnumerable<PayrollDTO>> GetPayrollbyDate(int month, int year);
        public Task<string> DeletePayrollforemployee(string Employeeid);
        public Task<string> AddPayrollforEmployee(AddPayrollDTO payroll);
        public Task<string> UpdatePayrollforEmployee(EditPayrollDTO editpayroll);
        public Task<decimal> CalculatePayroll(PayrollDateDTO payrollDate);
    }
}
