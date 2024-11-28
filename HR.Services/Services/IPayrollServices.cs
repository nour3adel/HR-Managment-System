using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.DTOs.Payroll;
using HR.Services.Bases;
namespace HR.Services.Services
{
    public interface IPayrollServices
    {
        public Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyEmployeeid(string Employeeid);
        public Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyDateforEmployee(string Employeeid, int month, int year);
        public Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyDate(int month, int year);
        public Task<Response<string>> DeletePayrollforemployee(string Employeeid);
        public Task<Response<string>> AddPayrollforEmployee(AddPayrollDTO payroll);
        public Task<Response<string>> UpdatePayrollforEmployee(EditPayrollDTO editpayroll);
        public Task<Response<string>> CalculatePayroll(PayrollDateDTO payrollDate);
    }
}
