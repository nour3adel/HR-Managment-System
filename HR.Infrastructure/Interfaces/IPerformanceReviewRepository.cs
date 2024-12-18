using HR.Domain.Classes;
using HR.Infrastructure.Common;

namespace HR.Infrastructure.Interfaces
{
    public interface IPerformanceReviewRepository : IGenericRepository<PerformanceReview>
    {
        public Task<IEnumerable<PerformanceReview>> GetByEmployeeID(string employeeId);
        public Task<PerformanceReview> GetByDateforEmployee(string Employeeid, int month, int year);

    }
}
