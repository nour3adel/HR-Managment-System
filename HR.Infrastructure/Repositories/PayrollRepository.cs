using HR.Domain.Classes;
using HR.Domain.DTOs.Attendance;
using HR.Domain.DTOs.Payroll;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        private DbSet<Payroll> Payrolls;
        public PayrollRepository(HRdbContext dbContext) : base(dbContext)
        {
            Payrolls = dbContext.Set<Payroll>();
        }
        public async Task<IEnumerable<Payroll>> GetByEmployeeID(string employeeId)
        {
            var PayrollRecords = await Payrolls
              .Where(a => a.EmployeeId == employeeId)
              .ToListAsync();
            return PayrollRecords;
        }

        public async Task<IEnumerable<Payroll>> GetByDate(int month, int year)
        {
            var PayrollRecords = await Payrolls.Where(a => a.Month == month && a.Year == year).ToListAsync();
            return PayrollRecords;
        }
        public async Task<IEnumerable<Payroll>> GetByDateforEmployee(string Employeeid, int month, int year)
        {
            var PayrollRecords = await Payrolls.Where(a => a.EmployeeId == Employeeid && a.Month == month && a.Year == year).ToListAsync();
            return PayrollRecords;
        }
    }
}
