using HR.Domain.Classes;
using HR.Infrastructure.Common;
using HR.Infrastructure.Context;
using HR.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private DbSet<Employee> employees;

        public EmployeeRepository(HRdbContext dbContext) : base(dbContext)
        {
            employees = dbContext.Set<Employee>();

        }

    }
}
