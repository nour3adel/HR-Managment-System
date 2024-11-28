using HR.Domain.Classes;
using HR.Domain.DTOs.Attendance;
using HR.Domain.DTOs.Payroll;
using HR.Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Interfaces
{
    public interface IPayrollRepository : IGenericRepository<Payroll>
    {
        public Task<IEnumerable<Payroll>> GetByEmployeeID(string employeeId);
        public Task<IEnumerable<Payroll>> GetByDate(int month, int year);
        public Task<IEnumerable<Payroll>> GetByDateforEmployee(string Employeeid, int month, int year);
    }
}
