using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Domain.DTOs.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services.Mapping.Payrolls
{
    public partial class PayrollMapper : Profile
    {
        public PayrollMapper()
        {
            AddUserMapping();
        }
        public void AddUserMapping()
        {
            CreateMap<AddPayrollDTO, Payroll>();
            CreateMap<EditPayrollDTO, Payroll>();
        }
    }
}
