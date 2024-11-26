using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Infrastructure.Interfaces;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HR.Services.Implementations
{
    public class PayrollServices : IPayrollServices
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IPayrollRepository payrollRepository;
        private readonly IMapper mapper;
        public PayrollServices(UserManager<Employee> _userManager, IPayrollRepository payrollRepository, IMapper mapper)
        {
            this._userManager = _userManager;
            this.payrollRepository = payrollRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<PayrollDTO>> GetPayrollbyEmployeeid(string Employeeid)
        {
            var payrolls = await payrollRepository.GetByEmployeeID(Employeeid);
            List<PayrollDTO> payrollDTOs = new List<PayrollDTO>();
            foreach (var payroll in payrolls)
            {
                var payrollDTO = new PayrollDTO()
                {
                    Id = payroll.Id,
                    EmployeeName = payroll.Employee.FullName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    Bonus = payroll.Bonus,
                    Deduction = payroll.Deduction,
                    NetSalary = payroll.Employee.Salary + payroll.Bonus - payroll.Deduction
                };
                payrollDTOs.Add(payrollDTO);
            }
            return payrollDTOs;
        }
        public async Task<IEnumerable<PayrollDTO>> GetPayrollbyDate(int month, int year)
        {
            var payrolls = await payrollRepository.GetByDate(month, year);
            List<PayrollDTO> payrollDTOs = new List<PayrollDTO>();
            foreach (var payroll in payrolls)
            {
                var payrollDTO = new PayrollDTO()
                {
                    Id = payroll.Id,
                    EmployeeName = payroll.Employee.FullName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    Bonus = payroll.Bonus,
                    Deduction = payroll.Deduction,
                    NetSalary = payroll.Employee.Salary + payroll.Bonus - payroll.Deduction
                };
                payrollDTOs.Add(payrollDTO);
            }
            return payrollDTOs;
        }
        public async Task<string> AddPayrollforEmployee(AddPayrollDTO payroll)
        {
            if (payroll == null)
                return "payroll is null";
            var proll = mapper.Map<Payroll>(payroll);
            await payrollRepository.AddAsync(proll);
            await payrollRepository.SaveChangesAsync();
            return "Created Succesful";
        }
        public async Task<string> UpdatePayrollforEmployee(EditPayrollDTO editpayroll)
        {
            var user = await _userManager.FindByIdAsync(editpayroll.EmployeeId);
            if (user == null)
            {
                return "Employee does not exist.";
            }
            var payrolls = await payrollRepository.GetByEmployeeID(editpayroll.EmployeeId);
            foreach (var payroll in payrolls)
            {
                if (payroll.Month == editpayroll.Month && payroll.Year == editpayroll.Year)
                    return "there is already payroll with this date";
            }
            var proll = mapper.Map<Payroll>(editpayroll);
            await payrollRepository.UpdateAsync(proll);
            return "Payroll Edited";
        }
        public async Task<decimal> CalculatePayroll(PayrollDateDTO payrollDate)
        {
            decimal result = 0;
            var month = payrollDate.startdate.Month;
            var year = payrollDate.startdate.Year;
            while (year < payrollDate.enddate.Year || (year == payrollDate.enddate.Year && month <= payrollDate.enddate.Month))
            {
                var payrolls = await payrollRepository.GetByDate(month, year);
                foreach (var payroll in payrolls)
                {
                    result += payroll.Employee.Salary + payroll.Bonus - payroll.Deduction;
                }
                if (month == 12)
                {
                    month = 1;
                    year ++;
                }
                else
                    month ++;
            }
            return result;
        }
        public async Task<string> DeletePayrollforemployee(string Employeeid)
        {
            var payrolls = await payrollRepository.GetByEmployeeID(Employeeid);
            if (payrolls == null || !payrolls.Any())
                return $"There is no Payroll history for Emplyee with id: {Employeeid}";
            var respayrolls = payrolls.ToList();
            await payrollRepository.DeleteRangeAsync(respayrolls);
            return $"Payroll history for Employee with id: {Employeeid} Deleted successfully";
        }
    }
}
