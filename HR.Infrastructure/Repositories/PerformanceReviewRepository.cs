using HR.Domain.Classes;
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
    public class PerformanceReviewRepository : GenericRepository<PerformanceReview>, IPerformanceReviewRepository
    {
        private DbSet<PerformanceReview> PerformanceReviews;
        public PerformanceReviewRepository(HRdbContext dbContext) : base(dbContext)
        {
            PerformanceReviews = dbContext.Set<PerformanceReview>();
        }
        public async Task<IEnumerable<PerformanceReview>> GetByEmployeeID(string employeeId)
        {
            var result = await PerformanceReviews.Where(p => p.EmployeeId  == employeeId).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PerformanceReview>> GetByDateforEmployee(string Employeeid, DateOnly date)
        {
            var result = await PerformanceReviews.Where(p => p.EmployeeId == Employeeid && p.Date == date).ToListAsync();
            return result;
        }
    }
}
