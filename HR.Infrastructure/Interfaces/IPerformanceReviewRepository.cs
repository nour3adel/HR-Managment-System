using HR.Domain.Classes;
using HR.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Interfaces
{
    public interface IPerformanceReviewRepository :IGenericRepository<PerformanceReview>
    {
        public Task<IEnumerable<PerformanceReview>> GetByEmployeeID(string employeeId);
        public Task<PerformanceReview> GetByDateforEmployee(string Employeeid, DateOnly date);
    }
}
