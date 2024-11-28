using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Payroll;
using HR.Infrastructure.Interfaces;
using HR.Infrastructure.Repositories;
using HR.Services.Bases;
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
    public class PayrollServices : ResponseHandler, IPayrollServices
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
        //TODO: Update Bonus - Deduction
        public async Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyEmployeeid(string Employeeid)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<IEnumerable<PayrollDTO>>("Employee does not exist.");
            }
            var payrolls = await payrollRepository.GetByEmployeeID(Employeeid);
            if (!payrolls.Any())
            {
                return NotFound<IEnumerable<PayrollDTO>>($"There is No Payroll History for Embloyee with id: {Employeeid}");
            }
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
            return Success<IEnumerable<PayrollDTO>>(payrollDTOs);
        }
        //TODO: Update Bonus - Deduction
        public async Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyDate(int month, int year)
        {
            if (month < 1 || month > 12)
                return BadRequest<IEnumerable<PayrollDTO>>("Month is not valid, Please Enter Month in Range(1,12)");
            if(year < 2020 || year > 2024)
                return BadRequest<IEnumerable<PayrollDTO>>("Year is not valid, The payroll exist for Years in Range(2020,2024)");
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
            if (payrollDTOs.Count == 0)
            {
                return BadRequest<IEnumerable<PayrollDTO>>($"There is No Payroll History for month: {month} in year: {year}");
            }
            return Success<IEnumerable<PayrollDTO>>(payrollDTOs);
        }
        public async Task<Response<IEnumerable<PayrollDTO>>> GetPayrollbyDateforEmployee(string Employeeid, int month, int year)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<IEnumerable<PayrollDTO>> ("Employee does not exist.");
            }
            if (month < 1 || month > 12)
                return BadRequest<IEnumerable<PayrollDTO>>("Month is not valid, Please Enter Month in Range(1,12)");
            if (year < 2020 || year > 2024)
                return BadRequest<IEnumerable<PayrollDTO>>("Year is not valid, The payroll exist for Years in Range(2020,2024)");

            var payrolls = await payrollRepository.GetByDateforEmployee(Employeeid, month, year);
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
            if (payrollDTOs.Count == 0)
            {
                return BadRequest<IEnumerable<PayrollDTO>>($"There is No Payroll History for Employee with id: {Employeeid} for month: {month} in year: {year}");
            }
            return Success<IEnumerable<PayrollDTO>>(payrollDTOs);
        }
        public async Task<Response<string>> AddPayrollforEmployee(AddPayrollDTO payroll)
        {
            var user = await _userManager.FindByIdAsync(payroll.EmployeeId);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var paroll = await payrollRepository.GetByIdAsync(payroll.Id);
            if (paroll != null)
            {
                return BadRequest<string>("Payroll id exist, please Enter valid id");
            }
            var payRoll = await payrollRepository.GetByDateforEmployee(payroll.EmployeeId, payroll.Month, payroll.Year);
            if (payRoll != null)
                return BadRequest<string>($"Payroll exist for employee with id: {payroll.EmployeeId} in Month: {payroll.Month} Year: {payroll.Year}");
            var proll = mapper.Map<Payroll>(payroll);
            await payrollRepository.AddAsync(proll);
            await payrollRepository.SaveChangesAsync();
            return Created<string>("Created Succesful");
        }
        public async Task<Response<string>> UpdatePayrollforEmployee(EditPayrollDTO editpayroll)
        {
            var user = await _userManager.FindByIdAsync(editpayroll.EmployeeId);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var payrolls = await payrollRepository.GetByEmployeeID(editpayroll.EmployeeId);
            foreach (var payroll in payrolls)
            {
                if (payroll.Month == editpayroll.Month && payroll.Year == editpayroll.Year)
                    return BadRequest<string>("there is already payroll with this date");
            }
            var proll = mapper.Map<Payroll>(editpayroll);
            await payrollRepository.UpdateAsync(proll);
            return Updated<string>("Payroll Edited");
        }
        public async Task<Response<string>> CalculatePayroll(PayrollDateDTO payrollDate)
        {
            if (payrollDate.enddate < payrollDate.startdate)
                return BadRequest<string>($"date is invalid, {payrollDate.enddate} < {payrollDate.startdate}");
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
            if (result == 0) return Success<string>("There is No payroll records for this date");
            return Success<string>($"Totla Payroll is {result} LE");
        }
        public async Task<Response<string>> DeletePayrollforemployee(string Employeeid)
        {
            var user = await _userManager.FindByIdAsync(Employeeid);
            if (user == null)
            {
                return NotFound<string>("Employee does not exist.");
            }
            var payrolls = await payrollRepository.GetByEmployeeID(Employeeid);
            if (!payrolls.Any())
                return NotFound<string>($"There is no Payroll history for Emplyee with id: {Employeeid}");
            var respayrolls = payrolls.ToList();
            await payrollRepository.DeleteRangeAsync(respayrolls);
            return Deleted<string>($"Payroll history for Employee with id: {Employeeid} Deleted successfully");
        }
    }
}
