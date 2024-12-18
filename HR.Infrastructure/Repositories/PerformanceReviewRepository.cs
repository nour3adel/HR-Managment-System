using HR.Domain.Classes;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var result = await PerformanceReviews.Where(p => p.EmployeeId == employeeId).ToListAsync();
            return result;
        }
        public async Task<PerformanceReview> GetByDateforEmployee(string Employeeid, int month, int year)
        {
            var result = PerformanceReviews.FirstOrDefault(p => p.EmployeeId == Employeeid && p.Date.Month == month && p.Date.Year == year);
            return result;
        }
    }
}
