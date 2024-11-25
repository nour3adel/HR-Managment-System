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
    public class PayrollRepository : GenericRepository<Attendance>, IPayrollRepository
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
            var PayrollRecords =  await Payrolls.Where(a => a.Month == month && a.Year == year).ToListAsync();
            return PayrollRecords;
        }
        public async Task DeleteRangeAsync(ICollection<Payroll> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        Task<List<Payroll>> IGenericRepository<Payroll>.Selectall()
        {
            throw new NotImplementedException();
        }

        Task<Payroll> IGenericRepository<Payroll>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<Payroll> IGenericRepository<Payroll>.GetTableNoTracking()
        {
            throw new NotImplementedException();
        }

        IQueryable<Payroll> IGenericRepository<Payroll>.GetTableAsTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<Payroll> AddAsync(Payroll entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task AddRangeAsync(ICollection<Payroll> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Payroll entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateRangeAsync(ICollection<Payroll> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Payroll entity)
        {
            throw new NotImplementedException();
        }
    }
}
